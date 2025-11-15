using AutoMapper;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class UserProfilesController : Controller
    {
        private readonly IUserProfileService _userProfileService;
        private readonly UserManager<Users> _userManager;
        private readonly IEventService _eventService;

        public UserProfilesController(IUserProfileService userProfileService, UserManager<Users> userManager, IEventService eventService)
        {
            _userProfileService = userProfileService;
            _userManager = userManager;
            _eventService = eventService;
        }
        // GET: UserProfiles/Profile (Current user's profile)
        public async Task<IActionResult> Profile()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");
                var profile = await _userProfileService.GetProfileByUserIdAsync(currentUser.Id);
                if (profile == null)
                {
                    //create a new profile if one doesn't exist
                    profile = new UserProfiles
                    {
                        UserId = currentUser.Id,
                        DisplayName = currentUser.UserName
                    };
                }
                //Get user's events for profile display
                var userEvents = await _eventService.GetEventsByUserIdAsync(currentUser.Id);
                ViewBag.UserEvents = userEvents.Take(5).ToList(); //show only recent 5
                ViewBag.TotalEvents = userEvents.Count();
                ViewBag.IsCurrentUser = true;

                return View(profile);
            }
            catch (Exception)
            {
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
                //check if profile is public or if current user is admin
                if (!profile.IsPublic && !User.IsInRole("Admin"))
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser == null || currentUser.Id != userId)
                    {
                        return Forbid();
                    }
                }
                //get user's public events
                var userEvents = await _eventService.GetEventsByUserIdAsync(userId);
                var publicEvents = userEvents.Where(e => !e.IsPrivate).Take(5).ToList();

                ViewBag.UserEvents = publicEvents;
                ViewBag.TotalPublicEvents = userEvents.Count(e => !e.IsPrivate);
                var currentViewingUser = await _userManager.GetUserAsync(User);
                ViewBag.IsCurrentUser = currentViewingUser?.Id == userId;


                return View("Profile", profile);
            }
            catch (Exception)
            {
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
                var profile = await _userProfileService.GetProfileByUserIdAsync(currentUser.Id);
                if (profile == null)
                {
                    //create a new profile if one doesn't exist
                    profile = new UserProfiles
                    {
                        UserId = currentUser.Id,
                        DisplayName = currentUser.UserName
                    };
                }
                return View(profile);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load profile for editing.";
                return RedirectToAction(nameof(Profile));
            }
        }
        // POST: UserProfiles/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserProfiles profile)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction("Login", "Account");

            // Remove validation for properties that shouldn't be validated
            ModelState.Remove("User");
            ModelState.Remove("ProfileImageFile");

            //ensure the profile belongs to the current user (security check)
            if (profile.UserProfileId != 0)
            {
                var existingProfile = await _userProfileService.GetProfileByUserIdAsync(currentUser.Id);
                if (existingProfile == null || existingProfile.UserProfileId != profile.UserProfileId)
                {
                    return Forbid();
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle file upload
                    if (profile.ProfileImageFile != null && profile.ProfileImageFile.Length > 0)
                    {
                        // Validate file type
                        var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
                        if (!allowedTypes.Contains(profile.ProfileImageFile.ContentType.ToLower()))
                        {
                            ModelState.AddModelError("ProfileImageFile", "Invalid file type. Please upload a JPEG, PNG, or GIF image.");
                            return View(profile);
                        }

                        // Validate file size (5MB max)
                        if (profile.ProfileImageFile.Length > 5 * 1024 * 1024)
                        {
                            ModelState.AddModelError("ProfileImageFile", "File too large. Please upload an image smaller than 5MB.");
                            return View(profile);
                        }

                        // Create wwwroot/images/profiles folder if it doesn't exist
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
                        Directory.CreateDirectory(uploadsFolder);

                        // Generate unique filename
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(profile.ProfileImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        // Save the file
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await profile.ProfileImageFile.CopyToAsync(fileStream);
                        }

                        // Delete old image if exists
                        if (!string.IsNullOrEmpty(profile.ProfileImageUrl) && profile.ProfileImageUrl.StartsWith("/images/"))
                        {
                            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profile.ProfileImageUrl.TrimStart('/'));
                            if (System.IO.File.Exists(oldImagePath))
                            {
                                System.IO.File.Delete(oldImagePath);
                            }
                        }

                        // Store the relative path in the database
                        profile.ProfileImageUrl = "/images/profiles/" + uniqueFileName;
                    }

                    profile.UserId = currentUser.Id;
                    UserProfiles? updatedProfile;

                    if (profile.UserProfileId == 0)
                    {
                        updatedProfile = await _userProfileService.CreateProfileAsync(profile);
                    }
                    else
                    {
                        //update existing profile
                        updatedProfile = await _userProfileService.UpdateProfileAsync(profile);
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
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occurred while updating the profile.";
                    System.Diagnostics.Debug.WriteLine($"Error updating profile: {ex.Message}");
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
                    TempData["Success"] = "Profile deleted successfully!";
                }
                else
                {
                    TempData["Error"] = "Profile not found or could not be deleted.";
                }
            }
            catch (Exception)
            {
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
                catch (Exception)
                {
                    TempData["Error"] = "Failed to search profiles.";
                    return View(results);
                }
            }
            ViewBag.SearchTerm = searchTerm;
            return View(results);
        }
        // GET: UserProfiles/SearchApi - Returns JSON for AJAX
        [HttpGet]
        public async Task<IActionResult> SearchApi(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || searchTerm.Length < 2)
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
                }).Take(10);
                return Json(results);
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
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
                //validate file type
                var allowedTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
                if (!allowedTypes.Contains(profileImage.ContentType.ToLower()))
                {
                    return Json(new { success = false, message = "Invalid file type. Please upload a JPEG, PNG, or GIF image." });
                }

                //validate file size (ex. 5MB max)
                if (profileImage.Length > 5 * 1024 * 1024)
                {
                    return Json(new { success = false, message = "File too large. Please upload an image smaller than 5MB." });
                }

                // Create wwwroot/images/profiles folder if it doesn't exist
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profiles");
                Directory.CreateDirectory(uploadsFolder);

                // Generate unique filename
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(profileImage.FileName);
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Save the file
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await profileImage.CopyToAsync(fileStream);
                }

                var imageUrl = "/images/profiles/" + uniqueFileName;

                //update user profile with new image url
                var profile = await _userProfileService.GetProfileByUserIdAsync(currentUser.Id);
                if (profile != null)
                {
                    // Delete old image if exists
                    if (!string.IsNullOrEmpty(profile.ProfileImageUrl) && profile.ProfileImageUrl.StartsWith("/images/"))
                    {
                        var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profile.ProfileImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    profile.ProfileImageUrl = imageUrl;
                    await _userProfileService.UpdateProfileAsync(profile);
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
                //check if profile is public or if current user has access
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
            catch (Exception)
            {
                return Json(new { success = false });
            }
        }
    }

}