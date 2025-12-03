using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using CS478_EventPlannerProject.Services; // For ContentModerationService

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly ICategoryService _categoryService;
        private readonly UserManager<Users> _userManager;
        private readonly ILogger<EventsController> _logger;

        // Constants
        private const int MaxBannerImageSizeBytes = 5 * 1024 * 1024; // 5MB
        private const string EventBannerFolder = "images/events";
        private const string DefaultBannerFolder = "images/banners";
        private static readonly string[] AllowedImageTypes = { "image/jpeg", "image/jpg", "image/png", "image/gif" };

        public EventsController(
            IEventService eventService,
            ICategoryService categoryService,
            UserManager<Users> userManager,
            ILogger<EventsController> logger)
        {
            _eventService = eventService;
            _categoryService = categoryService;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            try
            {
                var events = await _eventService.GetAllEventsAsync();
                return View(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load events");
                TempData["Error"] = "Failed to load events.";
                return View(new List<Events>());
            }
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var eventItem = await _eventService.GetEventByIdAsync(id);
                if (eventItem == null)
                {
                    _logger.LogWarning("Event not found: {EventId}", id);
                    return NotFound();
                }
                return View(eventItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load event details for EventId: {EventId}", id);
                TempData["Error"] = "Failed to load event details.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            var model = new Events
            {
                StartDateTime = DateTime.Now.AddDays(1),
                EndDateTime = DateTime.Now.AddDays(1).AddHours(2)
            };
            return View(model);
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Events eventModel)
        {
            // Remove validation for properties that are set automatically
            ModelState.Remove("CreatorId");
            ModelState.Remove("Creator");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("UpdatedAt");
            ModelState.Remove("BannerImageFile");

            // *** CONTENT MODERATION CHECK ***
            if (ContentModerationService.ViolatesGuidelines(
                eventModel.EventName,
                eventModel.EventDescription,
                eventModel.EventDetails))
            {
                ModelState.AddModelError("", ContentModerationService.GetViolationMessage());
                return View(eventModel);
            }

            if (!ModelState.IsValid)
            {
                LogValidationErrors();
                return View(eventModel);
            }

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    _logger.LogWarning("User not authenticated during event creation");
                    ModelState.AddModelError("", "User not found. Please log in again.");
                    return View(eventModel);
                }

                eventModel.CreatorId = currentUser.Id;
                eventModel.CreatedAt = DateTime.UtcNow;
                eventModel.UpdatedAt = DateTime.UtcNow;

                // Handle banner image
                await HandleEventBannerAsync(eventModel);

                await _eventService.CreateEventAsync(eventModel);
                _logger.LogInformation("Event created: {EventId} by user {UserId}", eventModel.EventId, currentUser.Id);

                TempData["Success"] = "Event created successfully!";
                return RedirectToAction(nameof(MyEvents));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating event");
                ModelState.AddModelError("", "An error occurred while creating the event.");
                return View(eventModel);
            }
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var eventItem = await _eventService.GetEventByIdAsync(id);
                if (eventItem == null)
                {
                    _logger.LogWarning("Event not found for edit: {EventId}", id);
                    return NotFound();
                }

                if (!await IsAuthorizedToModifyEventAsync(eventItem))
                {
                    _logger.LogWarning("Unauthorized edit attempt on event {EventId}", id);
                    return Forbid();
                }

                return View(eventItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load event for editing: {EventId}", id);
                TempData["Error"] = "Failed to load event for editing.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Events eventModel)
        {
            if (id != eventModel.EventId)
            {
                _logger.LogWarning("Event ID mismatch: route={RouteId}, model={ModelId}", id, eventModel.EventId);
                return NotFound();
            }

            // Remove validation for properties that shouldn't be validated
            ModelState.Remove("Creator");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("UpdatedAt");
            ModelState.Remove("BannerImageFile");

            // *** CONTENT MODERATION CHECK ***
            if (ContentModerationService.ViolatesGuidelines(
                eventModel.EventName,
                eventModel.EventDescription,
                eventModel.EventDetails))
            {
                ModelState.AddModelError("", ContentModerationService.GetViolationMessage());
                return View(eventModel);
            }

            if (!ModelState.IsValid)
            {
                LogValidationErrors();
                return View(eventModel);
            }

            try
            {
                var existingEvent = await _eventService.GetEventByIdAsync(id);
                if (existingEvent == null)
                {
                    return NotFound();
                }

                if (!await IsAuthorizedToModifyEventAsync(existingEvent))
                {
                    _logger.LogWarning("Unauthorized edit attempt on event {EventId}", id);
                    return Forbid();
                }

                // Handle banner image
                await HandleEventBannerAsync(eventModel);

                eventModel.UpdatedAt = DateTime.UtcNow;
                var updatedEvent = await _eventService.UpdateEventAsync(eventModel);

                if (updatedEvent != null)
                {
                    _logger.LogInformation("Event updated: {EventId}", eventModel.EventId);
                    TempData["Success"] = "Event updated successfully!";
                    return RedirectToAction(nameof(Details), new { id = eventModel.EventId });
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating event {EventId}", id);
                ModelState.AddModelError("", "An error occurred while updating the event.");
                return View(eventModel);
            }
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var eventItem = await _eventService.GetEventByIdAsync(id);
                if (eventItem == null)
                {
                    _logger.LogWarning("Event not found for delete: {EventId}", id);
                    return NotFound();
                }

                if (!await IsAuthorizedToModifyEventAsync(eventItem))
                {
                    _logger.LogWarning("Unauthorized delete attempt on event {EventId}", id);
                    return Forbid();
                }

                return View(eventItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load event for deletion: {EventId}", id);
                TempData["Error"] = "Failed to load event.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var eventItem = await _eventService.GetEventByIdAsync(id);
                if (eventItem == null)
                {
                    _logger.LogWarning("Event not found for delete confirmation: {EventId}", id);
                    return NotFound();
                }

                if (!await IsAuthorizedToModifyEventAsync(eventItem))
                {
                    _logger.LogWarning("Unauthorized delete confirmation on event {EventId}", id);
                    return Forbid();
                }

                // Delete banner image if it's a custom upload
                DeleteEventBanner(eventItem.BannerImageUrl);

                await _eventService.DeleteEventAsync(id);
                _logger.LogInformation("Event deleted: {EventId}", id);

                TempData["Success"] = "Event deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting event {EventId}", id);
                TempData["Error"] = "An error occurred while deleting the event.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Events/MyEvents
        public async Task<IActionResult> MyEvents()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    _logger.LogWarning("Unauthenticated user attempted to access MyEvents");
                    return RedirectToAction("Login", "Account");
                }

                var myEvents = await _eventService.GetEventsByUserIdAsync(currentUser.Id);
                return View(myEvents);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load user's events");
                TempData["Error"] = "Failed to load your events.";
                return View(new List<Events>());
            }
        }

        // POST: Events/Join/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(int id)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    _logger.LogWarning("Unauthenticated user attempted to join event");
                    return RedirectToAction("Login", "Account");
                }

                var success = await _eventService.AddAttendeeAsync(id, currentUser.Id);
                if (success)
                {
                    _logger.LogInformation("User {UserId} joined event {EventId}", currentUser.Id, id);
                    TempData["Success"] = "Successfully joined the event!";
                }
                else
                {
                    _logger.LogWarning("User {UserId} failed to join event {EventId} - may already be registered", currentUser.Id, id);
                    TempData["Error"] = "Could not join the event. You may already be registered.";
                }

                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error joining event {EventId}", id);
                TempData["Error"] = "An error occurred while joining the event.";
                return RedirectToAction("Details", new { id });
            }
        }

        // POST: Events/UpdateRSVP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRSVP(int eventId, string status)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    _logger.LogWarning("Unauthenticated user attempted to update RSVP");
                    return RedirectToAction("Login", "Account");
                }

                var success = await _eventService.UpdateAttendeeStatusAsync(eventId, currentUser.Id, status);
                if (success)
                {
                    _logger.LogInformation("User {UserId} updated RSVP to {Status} for event {EventId}", currentUser.Id, status, eventId);
                    TempData["Success"] = $"RSVP updated to {status}!";
                }
                else
                {
                    _logger.LogWarning("Failed to update RSVP for user {UserId} on event {EventId}", currentUser.Id, eventId);
                    TempData["Error"] = "Could not update RSVP";
                }

                return RedirectToAction("Details", new { id = eventId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating RSVP for event {EventId}", eventId);
                TempData["Error"] = "An error occurred while updating your RSVP.";
                return RedirectToAction("Details", new { id = eventId });
            }
        }

        // GET: Events/Attendees/5
        public async Task<IActionResult> Attendees(int id)
        {
            try
            {
                var eventItem = await _eventService.GetEventByIdAsync(id);
                if (eventItem == null)
                {
                    _logger.LogWarning("Event not found for attendees view: {EventId}", id);
                    return NotFound();
                }

                if (!await IsAuthorizedToModifyEventAsync(eventItem))
                {
                    _logger.LogWarning("Unauthorized access to attendees list for event {EventId}", id);
                    return Forbid();
                }

                var attendees = await _eventService.GetEventAttendeesAsync(id);
                ViewBag.Event = eventItem;
                return View(attendees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load attendees for event {EventId}", id);
                TempData["Error"] = "Failed to load attendees.";
                return RedirectToAction("Details", new { id });
            }
        }

        // GET: Events/Search
        public async Task<IActionResult> Search(
            string? searchTerm = null,
            int? categoryId = null,
            string? location = null,
            string? eventType = null,
            string? dateRange = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            try
            {
                // Get categories for the dropdown
                var categories = await _categoryService.GetAllCategoriesAsync();
                ViewBag.Categories = categories.ToList();

                // Perform search
                var events = await _eventService.SearchEventsAsync(
                    searchTerm,
                    categoryId,
                    location,
                    eventType,
                    dateRange,
                    startDate,
                    endDate);

                // Pass search parameters back to view for display
                ViewBag.SearchTerm = searchTerm;
                ViewBag.CategoryId = categoryId;
                ViewBag.Location = location;
                ViewBag.EventType = eventType;
                ViewBag.DateRange = dateRange;
                ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
                ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

                return View(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching events with term: {SearchTerm}", searchTerm);
                TempData["Error"] = "An error occurred while searching events.";
                return View(new List<Events>());
            }
        }

        // POST: Events/RemoveAttendee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAttendee(int eventId, string userId)
        {
            try
            {
                var eventItem = await _eventService.GetEventByIdAsync(eventId);
                if (eventItem == null)
                {
                    _logger.LogWarning("Event not found for attendee removal: {EventId}", eventId);
                    return NotFound();
                }

                if (!await IsAuthorizedToModifyEventAsync(eventItem))
                {
                    _logger.LogWarning("Unauthorized attendee removal attempt on event {EventId}", eventId);
                    return Forbid();
                }

                var success = await _eventService.RemoveAttendeeAsync(eventId, userId);
                if (success)
                {
                    _logger.LogInformation("Removed attendee {UserId} from event {EventId}", userId, eventId);
                    TempData["Success"] = "Attendee removed successfully!";
                }
                else
                {
                    _logger.LogWarning("Failed to remove attendee {UserId} from event {EventId}", userId, eventId);
                    TempData["Error"] = "Could not remove attendee.";
                }

                return RedirectToAction("Attendees", new { id = eventId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing attendee {UserId} from event {EventId}", userId, eventId);
                TempData["Error"] = "An error occurred while removing the attendee.";
                return RedirectToAction("Attendees", new { id = eventId });
            }
        }

        // GET: Events/GetAvailableBanners - Returns list of default banners
        [HttpGet]
        public IActionResult GetAvailableBanners()
        {
            try
            {
                var bannersFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", DefaultBannerFolder);

                if (!Directory.Exists(bannersFolder))
                {
                    _logger.LogWarning("Banners folder does not exist: {Path}", bannersFolder);
                    return Json(new List<string>());
                }

                var bannerFiles = Directory.GetFiles(bannersFolder)
                    .Where(f => AllowedImageTypes.Any(ext => f.EndsWith(ext.Replace("image/", "."), StringComparison.OrdinalIgnoreCase)))
                    .Select(f => $"/{DefaultBannerFolder}/" + Path.GetFileName(f))
                    .OrderBy(f => f)
                    .ToList();

                return Json(bannerFiles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading available banners");
                return Json(new List<string>());
            }
        }

        #region Private Helper Methods

        /// <summary>
        /// Handles event banner image updates with priority: selected banner > custom upload > existing
        /// </summary>
        private async Task HandleEventBannerAsync(Events eventModel)
        {
            var selectedBanner = Request.Form["SelectedBanner"].ToString();

            // Priority 1: Handle selected default banner
            if (!string.IsNullOrEmpty(selectedBanner) && selectedBanner.StartsWith($"/{DefaultBannerFolder}/"))
            {
                // Delete old custom uploaded image if exists (but not default banners)
                if (!string.IsNullOrEmpty(eventModel.BannerImageUrl) &&
                    eventModel.BannerImageUrl.StartsWith($"/{EventBannerFolder}/"))
                {
                    DeleteEventBanner(eventModel.BannerImageUrl);
                }

                eventModel.BannerImageUrl = selectedBanner;
                return;
            }

            // Priority 2: Handle custom file upload
            if (eventModel.BannerImageFile != null && eventModel.BannerImageFile.Length > 0)
            {
                // Validate file type
                if (!AllowedImageTypes.Contains(eventModel.BannerImageFile.ContentType.ToLower()))
                {
                    ModelState.AddModelError("BannerImageFile", "Invalid file type. Please upload a JPEG, PNG, or GIF image.");
                    return;
                }

                // Validate file size
                if (eventModel.BannerImageFile.Length > MaxBannerImageSizeBytes)
                {
                    ModelState.AddModelError("BannerImageFile", "File too large. Please upload an image smaller than 5MB.");
                    return;
                }

                // Delete old custom upload if exists
                if (!string.IsNullOrEmpty(eventModel.BannerImageUrl) &&
                    eventModel.BannerImageUrl.StartsWith($"/{EventBannerFolder}/"))
                {
                    DeleteEventBanner(eventModel.BannerImageUrl);
                }

                // Save new banner
                eventModel.BannerImageUrl = await SaveEventBannerAsync(eventModel.BannerImageFile);
            }

            // Priority 3: Keep existing banner (no action needed)
        }

        /// <summary>
        /// Saves an event banner image file and returns the URL path
        /// </summary>
        private async Task<string> SaveEventBannerAsync(IFormFile bannerFile)
        {
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", EventBannerFolder);
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(bannerFile.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await bannerFile.CopyToAsync(fileStream);
            }

            return $"/{EventBannerFolder}/" + uniqueFileName;
        }

        /// <summary>
        /// Deletes an event banner image if it's a custom upload
        /// </summary>
        private void DeleteEventBanner(string? bannerUrl)
        {
            if (!string.IsNullOrEmpty(bannerUrl) && bannerUrl.StartsWith($"/{EventBannerFolder}/"))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", bannerUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    try
                    {
                        System.IO.File.Delete(imagePath);
                        _logger.LogInformation("Deleted event banner: {ImagePath}", imagePath);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Could not delete event banner: {ImagePath}", imagePath);
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the current user is authorized to modify the event
        /// </summary>
        private async Task<bool> IsAuthorizedToModifyEventAsync(Events eventItem)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser?.Id == eventItem.CreatorId || User.IsInRole("Admin");
        }

        /// <summary>
        /// Logs validation errors for debugging
        /// </summary>
        private void LogValidationErrors()
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                _logger.LogWarning("Validation error: {ErrorMessage}", error.ErrorMessage);
            }
        }

        #endregion
    }
}