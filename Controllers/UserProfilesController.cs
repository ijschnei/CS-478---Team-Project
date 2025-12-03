using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly UserManager<Users> _userManager;
        private readonly IEventService _eventService;
        private readonly ILogger<UserProfilesController> _logger;

        // Constants
        private const int RecentEventsDisplayCount = 5;
        private const int MaxProfileImageSizeBytes = 5 * 1024 * 1024; // 5MB
        private const int MinSearchTermLength = 2;
        private const int MaxSearchResults = 10;
        private static readonly string[] AllowedImageTypes = { "image/jpeg", "image/jpg", "image/png", "image/gif" };

        public UserProfilesController(
            IUserProfileService userProfileService,
            UserManager<Users> userManager,
            IEventService eventService,
            ILogger<UserProfilesController> logger)
        {
            _userProfileService = userProfileService;
            _userManager = userManager;
            _eventService = eventService;
            _logger = logger;
        }

        // GET: UserProfiles/Profile (Current user's profile)
        public async Task<IActionResult> Profile()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");

                var profile = await GetOrCreateProfileAsync(currentUser.Id, currentUser.UserName);

                // Get user's events for profile display
                var userEvents = await _eventService.GetEventsByUserIdAsync(currentUser.Id);
                ViewBag.UserEvents = userEvents.Take(RecentEventsDisplayCount).ToList();
                ViewBag.TotalEvents = userEvents.Count();
                ViewBag.IsCurrentUser = true;

                return View(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load profile for user");
                TempData["Error"] = "Failed to load profile.";
                return RedirectToAction("Index", "Dashboard");
            }
        }

        // GET: UserProfiles/View/userId (View another user's public profile)
        public async Task<IActionResult> View(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return BadRequest();

            try
            {
                var profile = await _userProfileService.GetProfileByUserIdAsync(userId);
                if (profile == null)
                    return NotFound();

                // Check if profile is public or if current user is admin/owner
                if (!profile.IsPublic && !User.IsInRole("Admin"))
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser == null || currentUser.Id != userId)
                    {
                        return Forbid();
                    }
                }

                // Get user's public events
                var userEvents = await _eventService.GetEventsByUserIdAsync(userId);
                var publicEvents = userEvents.Where(e => !e.IsPrivate).Take(RecentEventsDisplayCount).ToList();

                ViewBag.UserEvents = publicEvents;
                ViewBag.TotalPublicEvents = userEvents.Count(e => !e.IsPrivate);

                var currentViewingUser = await _userManager.GetUserAsync(User);
                ViewBag.IsCurrentUser = currentViewingUser?.Id == userId;

                return View("Profile", profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load user profile for userId: {UserId}", userId);
                TempData["Error"] = "Failed to load user profile.";
                return RedirectToAction("Index", "Dashboard");
            }
        }

        // GET: UserProfiles/Edit
        public async Task<IActionResult> Edit()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");

                var profile = await GetOrCreateProfileAsync(currentUser.Id, currentUser.UserName);
                return View(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load profile for editing");
                TempData["Error"] = "Failed to load profile for editing.";
                return RedirectToAction(nameof(Profile));
            }
        }

        // POST: UserProfiles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfiles profile, string? SelectedAvatar)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction("Login", "Account");

            // Remove validation for properties that shouldn't be validated
            ModelState.Remove("User");
            ModelState.Remove("ProfileImageFile");
            ModelState.Remove("SelectedAvatar");

            // Ensure the profile belongs to the current user (security check)
            if (profile.UserProfileId != 0)
            {
                var existingProfile = await _userProfileService.GetProfileByUserIdAsync(currentUser.Id);
                if (existingProfile == null || existingProfile.UserProfileId != profile.UserProfileId)
                {
                    _logger.LogWarning("Unauthorized profile edit attempt by user {UserId}", currentUser.Id);
                    return Forbid();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle profile image with priority order
                    await HandleProfileImageAsync(profile, SelectedAvatar);

                    profile.UserId = currentUser.Id;
                    UserProfiles? updatedProfile;

                    if (profile.UserProfileId == 0)
                    {
                        updatedProfile = await _userProfileService.CreateProfileAsync(profile);
                        _logger.LogInformation("Created new profile for user {UserId}", currentUser.Id);
                    }
                    else
                    {
                        updatedProfile = await _userProfileService.UpdateProfileAsync(profile);
                        _logger.LogInformation("Updated profile for user {UserId}", currentUser.Id);
                    }

                    if (updatedProfile != null)
                    {
                        TempData["Success"] = "Profile updated successfully!";
                        return RedirectToAction(nameof(Profile));
                    }

                    TempData["Error"] = "Failed to update profile.";
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex, "InvalidOperationException while updating profile");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex, "ArgumentException while updating profile");
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the profile.";
                    _logger.LogError(ex, "Error updating profile for user {UserId}", currentUser.Id);
                }
            }
            else
            {
                // Log validation errors
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        _logger.LogWarning("Validation error: {ErrorMessage}", error.ErrorMessage);
                    }
                }
            }

            return View(profile);
        }

        // GET: UserProfiles/Delete
        public async Task<IActionResult> Delete()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction("Login", "Account");

            var profile = await _userProfileService.GetProfileByUserIdAsync(currentUser.Id);
            if (profile == null)
                return NotFound();

            return View(profile);
        }

        // POST: UserProfiles/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");

                var success = await _userProfileService.DeleteProfileAsync(currentUser.Id);
                if (success)
                {
                    _logger.LogInformation("Profile deleted for user {UserId}", currentUser.Id);
                    TempData["Success"] = "Profile deleted successfully!";
                }
                else
                {
                    _logger.LogWarning("Failed to delete profile for user {UserId}", currentUser.Id);
                    TempData["Error"] = "Profile not found or could not be deleted.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting profile");
                TempData["Error"] = "An error occurred while deleting the profile.";
                return RedirectToAction(nameof(Profile));
            }

            return RedirectToAction("Index", "Home");
        }

        // GET: UserProfiles/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            var results = new List<UserProfiles>();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                try
                {
                    results = (await _userProfileService.SearchProfilesAsync(searchTerm)).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to search profiles with term: {SearchTerm}", searchTerm);
                    TempData["Error"] = "Failed to search profiles.";
                }
            }

            ViewBag.SearchTerm = searchTerm;
            return View(results);
        }

        // GET: UserProfiles/SearchApi - Returns JSON for AJAX
        [HttpGet]
        public async Task<IActionResult> SearchApi(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < MinSearchTermLength)
            {
                return Json(new List<object>());
            }

            try
            {
                var profiles = await _userProfileService.SearchProfilesAsync(searchTerm);
                var results = profiles.Select(p => new
                {
                    userId = p.UserId,
                    fullName = p.FullName,
                    displayName = p.DisplayName
                }).Take(MaxSearchResults);

                return Json(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SearchApi failed for term: {SearchTerm}", searchTerm);
                return Json(new List<object>());
            }
        }

        // GET: UserProfiles/Browse
        public async Task<IActionResult> Browse()
        {
            try
            {
                var profiles = await _userProfileService.GetAllPublicProfilesAsync();
                return View(profiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load profiles for browse");
                TempData["Error"] = "Failed to load profiles.";
                return View(new List<UserProfiles>());
            }
        }

        // POST: UserProfiles/UploadProfileImage (AJAX endpoint for profile image upload)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadProfileImage(IFormFile profileImage)
        {
            if (profileImage == null || profileImage.Length == 0)
            {
                return Json(new { success = false, message = "No file selected" });
            }

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return Json(new { success = false, message = "User not authenticated." });

                // Validate file type
                if (!AllowedImageTypes.Contains(profileImage.ContentType.ToLower()))
                {
                    return Json(new { success = false, message = "Invalid file type. Please upload a JPEG, PNG, or GIF image." });
                }

                // Validate file size
                if (profileImage.Length > MaxProfileImageSizeBytes)
                {
                    return Json(new { success = false, message = "File too large. Please upload an image smaller than 5MB." });
                }

                var imageUrl = await SaveProfileImageAsync(profileImage);

                // Update user profile with new image url
                var profile = await _userProfileService.GetProfileByUserIdAsync(currentUser.Id);
                if (profile != null)
                {
                    // Delete old image if exists
                    DeleteOldProfileImage(profile.ProfileImageUrl);

                    profile.ProfileImageUrl = imageUrl;
                    await _userProfileService.UpdateProfileAsync(profile);
                    _logger.LogInformation("Profile image uploaded for user {UserId}", currentUser.Id);
                }

                return Json(new
                {
                    success = true,
                    message = "Profile image uploaded successfully!",
                    imageUrl = imageUrl
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading profile image");
                return Json(new { success = false, message = "An error occurred while uploading the image: " + ex.Message });
            }
        }

        // GET: UserProfiles/GetProfileData (AJAX endpoint)
        [HttpGet]
        public async Task<IActionResult> GetProfileData(string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser == null)
                        return Json(new { success = false });
                    userId = currentUser.Id;
                }

                var profile = await _userProfileService.GetProfileByUserIdAsync(userId);
                if (profile == null)
                    return Json(new { success = false });

                // Check if profile is public or if current user has access
                if (!profile.IsPublic)
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser == null || (currentUser.Id != userId && !User.IsInRole("Admin")))
                    {
                        return Json(new { success = false });
                    }
                }

                var result = new
                {
                    success = true,
                    data = new
                    {
                        fullName = profile.FullName,
                        displayName = profile.DisplayName,
                        bio = profile.Bio,
                        location = profile.Location,
                        profileImageUrl = profile.ProfileImageUrl,
                        website = profile.Website,
                        isPublic = profile.IsPublic
                    }
                };

                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get profile data for userId: {UserId}", userId);
                return Json(new { success = false });
            }
        }

        // GET: UserProfiles/GetAvailableAvatars (AJAX endpoint)
        [HttpGet]
        public IActionResult GetAvailableAvatars()
        {
            try
            {
                var avatarsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "avatars");

                if (!Directory.Exists(avatarsFolder))
                {
                    _logger.LogWarning("Avatars folder does not exist: {Path}", avatarsFolder);
                    return Json(new List<string>());
                }

                var avatarFiles = Directory.GetFiles(avatarsFolder)
                    .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    .Select(f => "/images/avatars/" + Path.GetFileName(f))
                    .ToList();

                return Json(avatarFiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading available avatars");
                return Json(new List<string>());
            }
        }

        #region Private Helper Methods

        /// <summary>
        /// Gets an existing profile or creates a new one if it doesn't exist
        /// </summary>
        private async Task<UserProfiles> GetOrCreateProfileAsync(string userId, string? defaultDisplayName)
        {
            var profile = await _userProfileService.GetProfileByUserIdAsync(userId);

            if (profile == null)
            {
                profile = new UserProfiles
                {
                    UserId = userId,
                    DisplayName = defaultDisplayName
                };
            }

            return profile;
        }

        /// <summary>
        /// Handles profile image updates with priority: selected avatar > custom upload > existing
        /// </summary>
        private async Task HandleProfileImageAsync(UserProfiles profile, string? selectedAvatar)
        {
            // Priority 1: Handle selected avatar (from default avatars)
            if (!string.IsNullOrEmpty(selectedAvatar))
            {
                profile.ProfileImageUrl = selectedAvatar;
                return;
            }

            // Priority 2: Handle custom file upload
            if (profile.ProfileImageFile != null && profile.ProfileImageFile.Length > 0)
            {
                // Validate file type
                if (!AllowedImageTypes.Contains(profile.ProfileImageFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("ProfileImageFile", "Invalid file type. Please upload a JPEG, PNG, or GIF image.");
                    return;
                }

                // Validate file size
                if (profile.ProfileImageFile.Length > MaxProfileImageSizeBytes)
                {
                    ModelState.AddModelError("ProfileImageFile", "File too large. Please upload an image smaller than 5MB.");
                    return;
                }

                // Delete old custom upload image if exists
                DeleteOldProfileImage(profile.ProfileImageUrl);

                // Save new image
                profile.ProfileImageUrl = await SaveProfileImageAsync(profile.ProfileImageFile);
            }

            // Priority 3: Keep existing image (no action needed)
        }

        /// <summary>
        /// Saves a profile image file and returns the URL path
        /// </summary>
        private async Task<string> SaveProfileImageAsync(IFormFile imageFile)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return "/images/profiles/" + uniqueFileName;
        }

        /// <summary>
        /// Deletes an old profile image if it's a custom upload
        /// </summary>
        private void DeleteOldProfileImage(string? imageUrl)
        {
            if (!string.IsNullOrEmpty(imageUrl) && imageUrl.StartsWith("/images/profiles/"))
            {
                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    try
                    {
                        System.IO.File.Delete(oldImagePath);
                        _logger.LogInformation("Deleted old profile image: {ImagePath}", oldImagePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Could not delete old image: {ImagePath}", oldImagePath);
                    }
                }
            }
        }

        #endregion
    }
}