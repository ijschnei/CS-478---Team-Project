using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using CS478_EventPlannerProject.Services;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly UserManager<Users> _userManager;

        public EventsController(IEventService eventService, UserManager<Users> userManager)
        {
            _eventService = eventService;
            _userManager = userManager;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEventsAsync();
            return View(events);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return View(eventItem);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            var model = new Events
            {
                StartDateTime = DateTime.Now.AddDays(1),
                EndDateTime = DateTime.Now.AddDays(1).AddHours(2)
            };

            // Pass venue data to the view
            ViewBag.Venues = VenueData.GetAllVenues();

            return View(model);
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Events eventModel, int? selectedVenueId, string? selectedTimeSlot)
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
                ViewBag.Venues = VenueData.GetAllVenues();
                return View(eventModel);
            }

            // Handle venue selection (if not virtual)
            if (!eventModel.IsVirtual && selectedVenueId.HasValue)
            {
                var venue = VenueData.GetVenueById(selectedVenueId.Value);
                if (venue != null)
                {
                    eventModel.VenueName = venue.Name;
                    eventModel.Address = venue.Address;
                    eventModel.City = venue.City;
                    eventModel.State = venue.State;
                    eventModel.Country = "United States";

                    // Store venue info in EventDetails for reference
                    eventModel.EventDetails = (eventModel.EventDetails ?? "") +
                        $"\n\nVenue Details:\nCapacity: {venue.Capacity} people\nAmenities: {venue.Amenities}";

                    if (!string.IsNullOrEmpty(selectedTimeSlot))
                    {
                        eventModel.EventDetails += $"\nSelected Time Slot: {selectedTimeSlot}";
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                {
                    eventModel.CreatorId = currentUser.Id;
                    eventModel.CreatedAt = DateTime.UtcNow;
                    eventModel.UpdatedAt = DateTime.UtcNow;

                    try
                    {
                        // Handle banner selection (if user selected a default banner)
                        if (!string.IsNullOrEmpty(Request.Form["SelectedBanner"]))
                        {
                            var selectedBanner = Request.Form["SelectedBanner"].ToString();

                            // Validate that the banner path is from the banners folder
                            if (selectedBanner.StartsWith("/images/banners/"))
                            {
                                // Delete old custom uploaded image if exists (but not default banners)
                                if (!string.IsNullOrEmpty(eventModel.BannerImageUrl) &&
                                    eventModel.BannerImageUrl.StartsWith("/images/events/"))
                                {
                                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eventModel.BannerImageUrl.TrimStart('/'));
                                    if (System.IO.File.Exists(oldImagePath))
                                    {
                                        System.IO.File.Delete(oldImagePath);
                                    }
                                }

                                eventModel.BannerImageUrl = selectedBanner;
                            }
                        }
                        // Handle file upload (if user uploaded custom image)
                        else if (eventModel.BannerImageFile != null && eventModel.BannerImageFile.Length > 0)
                        {
                            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "events");
                            Directory.CreateDirectory(uploadsFolder);

                            var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(eventModel.BannerImageFile.FileName);
                            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await eventModel.BannerImageFile.CopyToAsync(fileStream);
                            }

                            // Delete old image if exists (both custom and default)
                            if (!string.IsNullOrEmpty(eventModel.BannerImageUrl))
                            {
                                // Only delete if it's a custom uploaded image (in events folder)
                                if (eventModel.BannerImageUrl.StartsWith("/images/events/"))
                                {
                                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eventModel.BannerImageUrl.TrimStart('/'));
                                    if (System.IO.File.Exists(oldImagePath))
                                    {
                                        System.IO.File.Delete(oldImagePath);
                                    }
                                }
                            }

                            eventModel.BannerImageUrl = "/images/events/" + uniqueFileName;
                        }

                        await _eventService.CreateEventAsync(eventModel);
                        TempData["Success"] = "Event created successfully!";
                        return RedirectToAction(nameof(MyEvents));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Error creating event: {ex.Message}");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User not found. Please log in again.");
                }
            }
            else
            {
                // Log validation errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    System.Diagnostics.Debug.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }

            // Re-populate venues if returning to view
            ViewBag.Venues = VenueData.GetAllVenues();
            return View(eventModel);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            // Pass venue data to the view
            ViewBag.Venues = VenueData.GetAllVenues();

            return View(eventItem);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Events eventModel, int? selectedVenueId, string? selectedTimeSlot)
        {
            if (id != eventModel.EventId)
            {
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
                ViewBag.Venues = VenueData.GetAllVenues();
                return View(eventModel);
            }

            // Handle venue selection (if not virtual)
            if (!eventModel.IsVirtual && selectedVenueId.HasValue)
            {
                var venue = VenueData.GetVenueById(selectedVenueId.Value);
                if (venue != null)
                {
                    eventModel.VenueName = venue.Name;
                    eventModel.Address = venue.Address;
                    eventModel.City = venue.City;
                    eventModel.State = venue.State;
                    eventModel.Country = "United States";

                    // Update venue info in EventDetails
                    var detailsLines = (eventModel.EventDetails ?? "").Split('\n').ToList();
                    detailsLines.RemoveAll(l => l.Contains("Venue Details:") || l.Contains("Capacity:") ||
                                               l.Contains("Amenities:") || l.Contains("Selected Time Slot:"));

                    eventModel.EventDetails = string.Join("\n", detailsLines).Trim() +
                        $"\n\nVenue Details:\nCapacity: {venue.Capacity} people\nAmenities: {venue.Amenities}";

                    if (!string.IsNullOrEmpty(selectedTimeSlot))
                    {
                        eventModel.EventDetails += $"\nSelected Time Slot: {selectedTimeSlot}";
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser?.Id != eventModel.CreatorId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }

                try
                {
                    // Handle banner selection (if user selected a default banner)
                    if (!string.IsNullOrEmpty(Request.Form["SelectedBanner"]))
                    {
                        var selectedBanner = Request.Form["SelectedBanner"].ToString();

                        // Validate that the banner path is from the banners folder
                        if (selectedBanner.StartsWith("/images/banners/"))
                        {
                            // Delete old custom uploaded image if exists (but not default banners)
                            if (!string.IsNullOrEmpty(eventModel.BannerImageUrl) &&
                                eventModel.BannerImageUrl.StartsWith("/images/events/"))
                            {
                                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eventModel.BannerImageUrl.TrimStart('/'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }

                            eventModel.BannerImageUrl = selectedBanner;
                        }
                    }
                    // Handle file upload (if user uploaded custom image)
                    else if (eventModel.BannerImageFile != null && eventModel.BannerImageFile.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "events");
                        Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(eventModel.BannerImageFile.FileName);
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await eventModel.BannerImageFile.CopyToAsync(fileStream);
                        }

                        // Delete old image if exists
                        if (!string.IsNullOrEmpty(eventModel.BannerImageUrl))
                        {
                            // Only delete if it's a custom uploaded image (in events folder)
                            if (eventModel.BannerImageUrl.StartsWith("/images/events/"))
                            {
                                var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", eventModel.BannerImageUrl.TrimStart('/'));
                                if (System.IO.File.Exists(oldImagePath))
                                {
                                    System.IO.File.Delete(oldImagePath);
                                }
                            }
                        }

                        eventModel.BannerImageUrl = "/images/events/" + uniqueFileName;
                    }

                    var updatedEvent = await _eventService.UpdateEventAsync(eventModel);
                    if (updatedEvent != null)
                    {
                        TempData["Success"] = "Event updated successfully!";
                        return RedirectToAction(nameof(Details), new { id = eventModel.EventId });
                    }
                    return NotFound();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Error updating event: {ex.Message}");
                }
            }

            // Re-populate venues if returning to view
            ViewBag.Venues = VenueData.GetAllVenues();
            return View(eventModel);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            return View(eventItem);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            await _eventService.DeleteEventAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Events/MyEvents
        public async Task<IActionResult> MyEvents()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return RedirectToAction("Login", "Account");
            var myEvents = await _eventService.GetEventsByUserIdAsync(currentUser.Id);
            return View(myEvents);
        }

        // POST: Events/Join/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Join(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return RedirectToAction("Login", "Account");
            var success = await _eventService.AddAttendeeAsync(id, currentUser.Id);
            if (success)
            {
                TempData["Success"] = "Successfully joined the event!";
            }
            else
            {
                TempData["Error"] = "Could not join the event. You may already be registered.";
            }
            return RedirectToAction("Details", new { id });
        }

        // POST: Events/UpdateRSVP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRSVP(int eventId, string status)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return RedirectToAction("Login", "Account");
            var success = await _eventService.UpdateAttendeeStatusAsync(eventId, currentUser.Id, status);
            if (success)
            {
                TempData["Success"] = $"RSVP updated to {status}!";
            }
            else
            {
                TempData["Error"] = "Could not update RSVP";
            }
            return RedirectToAction("Details", new { id = eventId });
        }

        // GET: Events/Attendees/5
        public async Task<IActionResult> Attendees(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null) return NotFound();
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }
            var attendees = await _eventService.GetEventAttendeesAsync(id);
            ViewBag.Event = eventItem;
            return View(attendees);
        }

        // GET: Events/Search
        public async Task<IActionResult> Search(string searchTerm, int? categoryId)
        {
            var events = await _eventService.SearchEventsAsync(searchTerm, categoryId);
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            return View(events);
        }

        // POST: Events/RemoveAttendee
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveAttendee(int eventId, string userId)
        {
            var eventItem = await _eventService.GetEventByIdAsync(eventId);
            if (eventItem == null) return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            var success = await _eventService.RemoveAttendeeAsync(eventId, userId);
            if (success)
            {
                TempData["Success"] = "Attendee removed successfully!";
            }
            else
            {
                TempData["Error"] = "Could not remove attendee.";
            }
            return RedirectToAction("Attendees", new { id = eventId });
        }

        // GET: Events/GetVenueDetails - AJAX endpoint for getting venue details
        [HttpGet]
        public IActionResult GetVenueDetails(int venueId)
        {
            var venue = VenueData.GetVenueById(venueId);
            if (venue == null)
            {
                return Json(new { success = false });
            }

            return Json(new
            {
                success = true,
                venue = new
                {
                    id = venue.Id,
                    name = venue.Name,
                    type = venue.Type,
                    capacity = venue.Capacity,
                    address = venue.Address,
                    city = venue.City,
                    state = venue.State,
                    amenities = venue.Amenities,
                    timeSlots = venue.TimeSlots.Select(ts => new
                    {
                        date = ts.Date,
                        time = ts.Time,
                        isAvailable = ts.IsAvailable,
                        displayText = ts.DisplayText
                    }).ToList()
                }
            });
        }

        // GET: Events/GetAvailableBanners - Returns list of default banners
        [HttpGet]
        public IActionResult GetAvailableBanners()
        {
            try
            {
                var bannersFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "banners");

                if (!Directory.Exists(bannersFolder))
                {
                    return Json(new List<string>());
                }

                var bannerFiles = Directory.GetFiles(bannersFolder)
                    .Where(f => f.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                               f.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
                    .Select(f => "/images/banners/" + Path.GetFileName(f))
                    .OrderBy(f => f)
                    .ToList();

                return Json(bannerFiles);
            }
            catch (Exception)
            {
                return Json(new List<string>());
            }
        }
    }
}