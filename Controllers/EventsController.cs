using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;

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
            return View(model);
        }

        // POST: Events/Create
        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventName,EventDescription,EventDetails,StartDateTime,EndDateTime,TimeZone,IsAllDay,VenueName,Address,City,State,Country,PostalCode,IsVirtual,VirtualMeetingUrl,MaxAttendees,IsPrivate,RequiresApproval,AllowGuestList,ThemeId,CustomCss,BannerImageUrl")] Events eventModel)
        {
            // Remove validation for properties that are set automatically
            ModelState.Remove("CreatorId");
            ModelState.Remove("Creator");
            ModelState.Remove("CreatedAt");
            ModelState.Remove("UpdatedAt");

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
                    // This will show in the validation summary
                    System.Diagnostics.Debug.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }

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
            return View(eventItem);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventName,EventDescription,EventDetails,StartDateTime,EndDateTime,TimeZone,IsAllDay,VenueName,Address,City,State,Country,PostalCode,IsVirtual,VirtualMeetingUrl,MaxAttendees,IsPrivate,RequiresApproval,AllowGuestList,ThemeId,CustomCss,BannerImageUrl")] Events eventModel)
        {
            if (id != eventModel.EventId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser?.Id != eventModel.CreatorId && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }
                var updatedEvent = await _eventService.UpdateEventAsync(eventModel);
                if (updatedEvent != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(eventModel);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if(eventItem == null)
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
        public async Task <IActionResult> DeleteConfirmed(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if(eventItem == null)
            {
                return NotFound();
            }
            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser?.Id != eventItem.CreatorId && !User.IsInRole("Admin"))
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
        // POST : Events/Join/5
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

        //GET: Events/Search
        public async Task<IActionResult> Search(string searchTerm, int? categoryId)
        {
            var events = await _eventService.SearchEventsAsync(searchTerm, categoryId);
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CategoryId = categoryId;
            return View(events);
        }
    }
}
