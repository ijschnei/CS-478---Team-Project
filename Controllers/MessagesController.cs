using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using CS478_EventPlannerProject.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace CS478_EventPlannerProject.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<Users> _userManager;
        private readonly IEventService _eventService;
        private readonly IEventGroupMessageService _eventGroupMessageService;
        public MessagesController(IMessageService messageService, UserManager<Users> userManager, IEventService eventService, IEventGroupMessageService eventGroupMessageService)
        {
            _messageService = messageService;
            _userManager = userManager;
            _eventService = eventService;
            _eventGroupMessageService = eventGroupMessageService;
        }
        // GET: Messages
        public async Task<IActionResult> Index()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");
                var messages = await _messageService.GetUserMessagesAsync(currentUser.Id);

                //group messages by conversation
                var conversations = messages
                    .GroupBy(m => m.ConversationId)
                    .Select(g => new ConversationViewModel
                    {
                        ConversationId = g.Key,
                        LastMessage = g.OrderByDescending(m => m.SentAt).First(),
                        UnreadCount = g.Count(m => !m.IsRead && m.ReceiverId == currentUser.Id),
                        MessageCount = g.Count()
                    })
                    .OrderByDescending(c => c.LastMessage.SentAt)
                    .ToList();
                ViewBag.CurrentUserId = currentUser.Id;
                ViewBag.UnreadTotal = await _messageService.GetUnreadCountAsync(currentUser.Id);
                return View(conversations);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load messages.";
                return View(new List<ConversationViewModel>());
            }
        }
        // GET: Messages/Conversation/guid
        public async Task<IActionResult> Conversation(Guid conversationId)
        {
            if (conversationId == Guid.Empty)
                return BadRequest();
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");
                var messages = await _messageService.GetConversationAsync(conversationId);

                //verify user is part of this conversation
                if (!messages.Any(m => m.SenderId == currentUser.Id || m.ReceiverId == currentUser.Id))
                {
                    return Forbid();
                }

                //mark messages as read
                var unreadMessages = messages.Where(m => m.ReceiverId == currentUser.Id && !m.IsRead);
                foreach (var message in unreadMessages)
                {
                    await _messageService.MarkAsReadAsync(message.Id, currentUser.Id);
                }

                ViewBag.CurrentUserId = currentUser.Id;
                ViewBag.ConversationId = conversationId;

                //get the other participant for the header
                var otherParticipant = messages
                    .Where(m => m.SenderId != currentUser.Id)
                    .Select(m => m.Sender)
                    .FirstOrDefault() ??
                    messages
                    .Where(m => m.ReceiverId != currentUser.Id)
                    .Select(m => m.Receiver)
                    .FirstOrDefault();
                ViewBag.OtherParticipant = otherParticipant;
                return View(messages);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load conversation.";
                return RedirectToAction(nameof(Index));
            }
        }
        // GET: Messages/Compose
        public async Task<IActionResult> Compose(string? receiverId = null, int? eventId = null)
        {
            try
            {
                var model = new ComposeMessageViewModel();
                if (!string.IsNullOrEmpty(receiverId))
                {
                    var receiver = await _userManager.FindByIdAsync(receiverId);
                    if (receiver != null)
                    {
                        model.ReceiverId = receiverId;
                        model.ReceiverName = receiver.Profile?.FullName ?? receiver.UserName ?? "Unknown User";
                    }
                }
                if (eventId.HasValue)
                {
                    var eventItem = await _eventService.GetEventByIdAsync(eventId.Value);
                    if (eventItem != null)
                    {
                        model.RelatedEventId = eventId.Value;
                        model.EventName = eventItem.EventName;
                        model.MessageType = "event_related";
                    }
                }
                return View(model);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load compose form.";
                return RedirectToAction(nameof(Index));
            }
        }
        // POST: Messages/Compose
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Compose([Bind("ReceiverId,Subject,Content,MessageType,RelatedEventId")] ComposeMessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser == null)
                        return RedirectToAction("Login", "Account");
                    var message = new Messages
                    {
                        SenderId = currentUser.Id,
                        ReceiverId = model.ReceiverId,
                        Subject = model.Subject,
                        Content = model.Content,
                        MessageType = model.MessageType ?? "direct",
                        RelatedEventId = model.RelatedEventId
                    };
                    var sentMessage = await _messageService.SendMessageAsync(message);
                    TempData["Success"] = "Message sent successfully!";
                    return RedirectToAction(nameof(Conversation), new { conversationId = sentMessage.ConversationId });
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
                catch (Exception)
                {
                    TempData["Error"] = "An error occurred while sending the message.";
                }
            }
            //reload receiver and event data for view
            try
            {
                if (!string.IsNullOrEmpty(model.ReceiverId))
                {
                    var receiver = await _userManager.FindByIdAsync(model.ReceiverId);
                    if (receiver != null)
                    {
                        model.ReceiverName = receiver.Profile?.FullName ?? receiver.UserName ?? "Unknown User";
                    }
                }
                if (model.RelatedEventId.HasValue)
                {
                    var eventItem = await _eventService.GetEventByIdAsync(model.RelatedEventId.Value);
                    if (eventItem != null)
                    {
                        model.EventName = eventItem.EventName;
                    }
                }
            }
            catch { }
            return View(model);
        }
        // GET: Messages/EventMessages/5
        public async Task<IActionResult> EventMessages(int eventId)
        {
            if (eventId <= 0)
                return BadRequest();
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");
                //verify user has access to this event
                var eventItem = await _eventService.GetEventByIdAsync(eventId);
                if (eventItem == null)
                    return NotFound();

                //check if user is the creator or an attendee
                var isCreator = eventItem.CreatorId == currentUser.Id;
                var isAttendee = eventItem.Attendees.Any(a => a.UserId == currentUser.Id);
                if (!isCreator && !isAttendee && !User.IsInRole("Admin"))
                {
                    return Forbid();
                }
                var messages = await _messageService.GetEventMessagesAsync(eventId);
                ViewBag.Event = eventItem;
                ViewBag.CurrentUserId = currentUser.Id;
                return View(messages);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load event messages.";
                return RedirectToAction("Details", "Events", new { id = eventId });
            }
        }
        // POST: Messages/MarkAsRead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int messageId)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return Json(new { success = false, message = "User not authenticated." });
                var success = await _messageService.MarkAsReadAsync(messageId, currentUser.Id);
                if (success)
                {
                    return Json(new { success = true, message = "Message marked as read." });
                }
                return Json(new { success = false, message = "Failed to mark message as read." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred." });
            }
        }
        // GET: Messages/GetUnreadCount (AJAX endpoint)
        [HttpGet]
        public async Task<IActionResult> GetUnreadCount()
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return Json(new { count = 0 });
                var count = await _messageService.GetUnreadCountAsync(currentUser.Id);
                return Json(new { count });
            }
            catch (Exception)
            {
                return Json(new { count = 0 });
            }
        }
        // POST: Messages/SendQuickReply (AJAX endpoint)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendQuickReply([FromBody] QuickReplyModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Content) || model.ConversationId == Guid.Empty)
                return Json(new { success = false, message = "Invalid message data." });
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return Json(new { success = false, message = "User not authenticated." });
                //get the conversation to find the receiver
                var existingMessages = await _messageService.GetConversationAsync(model.ConversationId);
                var lastMessage = existingMessages.OrderByDescending(m => m.SentAt).FirstOrDefault();
                if (lastMessage == null)
                    return Json(new { success = false, message = "Conversation not found." });
                //determine receiver (the other participant)
                var receiverId = lastMessage.SenderId == currentUser.Id ?
                    lastMessage.ReceiverId : lastMessage.SenderId;

                var message = new Messages
                {
                    SenderId = currentUser.Id,
                    ReceiverId = receiverId,
                    Content = model.Content,
                    MessageType = model.MessageType ?? "direct",
                    ConversationId = model.ConversationId,
                    RelatedEventId = model.RelatedEventId
                };
                var sentMessage = await _messageService.SendMessageAsync(message);
                return Json(new
                {
                    success = true,
                    message = "Message sent succcessfully!",
                    messageId = sentMessage.Id,
                    timestamp = sentMessage.SentAt.ToString("yyyy-MM-dd HH:mm:ss")
                });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while sending the message." });
            }
        }

        //Group Event Messages Additions VVVV
        // GET: Messages/EventGroupChat/5
        public async Task<IActionResult> EventGroupChat(int eventId)
        {
            if (eventId <= 0)
                return BadRequest();
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");

                //verify user has access to this event
                var eventItem = await _eventService.GetEventByIdAsync(eventId);
                if (eventItem == null)
                    return NotFound();

                //check if user is the creator or an accepted attendee
                var isCreator = eventItem.CreatorId == currentUser.Id;
                var isAttendee = eventItem.Attendees.Any(a => a.UserId == currentUser.Id && a.Status == "accepted");

                if (!isCreator && !isAttendee && !User.IsInRole("Admin"))
                {
                    TempData["Error"] = "Only confirmed attendees can access the event group chat.";
                    return RedirectToAction("Details", "Events", new { id = eventId });
                }

                var messages = await _eventGroupMessageService.GetEventGroupMessagesAsync(eventId);
                var unreadCount = await _eventGroupMessageService.GetUnreadCountForEventAsync(eventId, currentUser.Id);

                ViewBag.Event = eventItem;
                ViewBag.CurrentUserId = currentUser.Id;
                ViewBag.IsCreator = isCreator;
                ViewBag.UnreadCount = unreadCount;

                return View(messages);
            }
            catch (Exception)
            {
                TempData["Error"] = "Failed to load event group chat.";
                return RedirectToAction("Details", "Events", new { id = eventId });
            }
        }

        // POST: Messages/SendGroupMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendGroupMessage(int eventId, string content, string? messageType = "chat")
        {
            if (eventId <= 0 || string.IsNullOrWhiteSpace(content))
                return Json(new { success = false, message = "Invalid message data." });

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return Json(new { success = false, message = "User not authenticated" });

                var message = new EventGroupMessages
                {
                    EventId = eventId,
                    SenderId = currentUser.Id,
                    Content = content,
                    MessageType = messageType ?? "chat"
                };
                var sentMessage = await _eventGroupMessageService.SendGroupMessageAsync(message);

                return Json(new
                {
                    success = true,
                    message = "Message sent successfully!",
                    messageId = sentMessage.Id,
                    timestamp = sentMessage.SentAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    senderName = currentUser.Profile?.FullName ?? currentUser.UserName,
                    messageType = sentMessage.MessageType
                });
            }
            catch (InvalidOperationException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while sending the message." });
            }
        }

        // POST: Messages/SendAnnouncement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendAnnouncement(int eventId, string subject, string content)
        {
            if (eventId <= 0 || string.IsNullOrWhiteSpace(content))
            {
                TempData["Error"] = "Subject and content are required.";
                return RedirectToAction(nameof(EventGroupChat), new { eventId });
            }

            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return RedirectToAction("Login", "Account");

                await _eventGroupMessageService.SendAnnouncementAsync(eventId, currentUser.Id, subject, content);
                TempData["Success"] = "Announcement sent successfully!";
                return RedirectToAction(nameof(EventGroupChat), new { eventId });
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(EventGroupChat), new { eventId });
            }
            catch (Exception)
            {
                TempData["Error"] = "An error occurred while sending the announcement.";
                return RedirectToAction(nameof(EventGroupChat), new { eventId });
            }
        }

        // POST: Messages/PinMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PinMessage(int messageId, int eventId)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return Json(new { success = false, message = "User is not authenticated." });

                var success = await _eventGroupMessageService.PinMessageAsync(messageId, currentUser.Id);
                if (success)
                    return Json(new { success = true, message = "Message pinned successfully." });

                return Json(new { success = false, message = "Failed to pin message." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred" });
            }
        }

        // POST: Messages/UnpinMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnpinMessage(int messageId, int eventId)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return Json(new { success = false, message = "User is not authenticated." });

                var success = await _eventGroupMessageService.UnpinMessageAsync(messageId, currentUser.Id);
                if (success)
                    return Json(new { success = true, message = "Message unpinned successfully." });

                return Json(new { success = false, message = "Failed to unpin message." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred" });
            }
        }

        // POST: Messages/DeleteGroupMessage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGroupMessage(int messageId, int eventId)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                    return Json(new { success = false, message = "User not authenticated." });

                var success = await _eventGroupMessageService.DeleteMessageAsync(messageId, currentUser.Id);
                if (success)
                    return Json(new { success = true, message = "Message deleted successfully." });

                return Json(new { success = false, message = "Failed to delete message." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred" });
            }
        }
    }

}