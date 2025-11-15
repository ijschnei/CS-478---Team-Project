using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class EventInvitationsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IMessageService _messageService;
        private readonly UserManager<Users> _userManager;
        
        public EventInvitationsController(
            IEventService eventService,
            IMessageService messageService,
            UserManager<Users> userManager )
        {
            _eventService = eventService;
            _messageService = messageService;
            _userManager = userManager;
        }
        
        // GET: EventInvitations/Send/5
        public async Task<IActionResult> Send(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null) 
                return NotFound();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser?.Id != eventItem.CreatorId)
                return Forbid();

            //get all users except creator and existing attendees
            var attendeeUserIds = eventItem.Attendees.Select(a => a.UserId).ToList();
            var availableUsers = _userManager.Users
                .Where(u=>u.Id != currentUser.Id && !attendeeUserIds.Contains(u.Id))
                .Select(u=> new SelectListItem
                {
                    Value = u.Id,
                    Text = u.Email
                }).ToList();
            ViewBag.Event = eventItem;
            ViewBag.AvailableUsers = availableUsers;

            return View();
        }

        // POST: EventInvitations/Send/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Send(int id, string[] selectedUsers, string customMessage)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction("Login", "Account");
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
                return NotFound();
            if (currentUser.Id != eventItem.CreatorId)
                return Forbid();
            if (selectedUsers == null || selectedUsers.Length == 0)
            {
                TempData["Error"] = "Please select at least one user to invite.";
                return RedirectToAction("Send", new { id });
            }
            try
            {
                int successCount = 0;
                foreach (var userId in selectedUsers)
                {
                    try
                    {
                        await _messageService.SendEventInvitationAsync(id, currentUser.Id, userId, customMessage);
                        successCount++;
                    }
                    catch
                    {
                        //skip users that couldn't be invited (already attending)
                        continue;
                    }
                }
                TempData["Success"] = $"Successfully sent {successCount} invitation(s)!";
                return RedirectToAction("Details", "Events", new { id });
            }catch(Exception ex)
            {
                TempData["Error"] = $"Failed to send invitations: {ex.Message}";
                return RedirectToAction("Send", new { id });
            }
        }

        // POST: EventInvitations/Accept/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Accept(int messageId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction("Login", "Account");
            try
            {
                var success = await _messageService.AcceptEventInvitationAsync(messageId, currentUser.Id);
                 if (success)
                {
                    TempData["Success"] = "You have successfully joined the event!";
                }
                else
                {
                    TempData["Error"] = "Could not accept invitation. You may already be registered.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Failed to accept invitation: {ex.Message}";
            }
            return RedirectToAction("Index", "Messages");
        }
    }
}
