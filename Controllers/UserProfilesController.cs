using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
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
                    if(currentUser == null || currentUser.Id != userId)
                    {
                        return Forbid();
                    }
                }
                //get user's public events
                var userEvents = await _eventService.GetEventsByUserIdAsync(userId);
                var publicEvents = userEvents.Where(e => !e.IsPrivate).Take(5).ToList();

                ViewBag.UserEvents = publicEvents;
                ViewBag.TotalPublicEvents = userEvents.Count(e => !e.IsPrivate);
                ViewBag.IsCurrentUser = false;

                return View("Profile", profile);
            } catch (Exception)
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
        public async Task<IActionResult> Edit([Bind("UserProfileId,UserId,FirstName,LastName,DisplayName,Bio,ProfileImageUrl,DateOfBirth,Location,Website,FacebookUrl,TwitterUrl,LinkedInUrl,IsPublic")]UserProfiles profile)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction("Login", "Account");
            //ensure the profile belongs to the current user (security check)
            if (profile.UserId != currentUser.Id)
                return Forbid();
            if (ModelState.IsValid)
            {
                try
                {
                    UserProfiles? updatedProfile;
                    if (profile.UserProfileId == 0)
                    {
                        //create new profile
                        profile.UserId = currentUser.Id;
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
                catch(InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch(ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while updating the profile.";
                }
            }
            return View(profile);
        }
        // POST: UserProfiles/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete()
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
            }
            return RedirectToAction("Index", "Dashboard");
        }
        // GET: UserProfiles/Search
        public async Task<IActionResult> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View(new List<UserProfiles>());
            }
            try
            {
                //TODO Add search method to IUserProfileService
                //returning empty results for now
                var profiles = new List<UserProfiles>();
                ViewBag.SearchTerm = searchTerm;
                return View(profiles);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to search profiles.";
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
                if(profileImage.Length > 5 * 1024 * 1024)
                {
                    return Json(new { success = false, message = "File too large. Please upload an image smaller than 5MB." });
                }

                //TODO: need to generate unique filename and save to storage and then updated profile with the new image url
                //for now using placeholder URL
                var imageUrl = "/images/profiles/placeholder.jpg"; //this would be actual uploaded image url
                //update user profile with new image url
                var profile = await _userProfileService.GetProfileByUserIdAsync(currentUser.Id);
                if(profile != null)
                {
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
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while uploading the image." });
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
                    if(currentUser == null || (currentUser.Id != userId && !User.IsInRole("Admin")))
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
