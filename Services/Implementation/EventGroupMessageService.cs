using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CS478_EventPlannerProject.Services.Implementation
{
    public class EventGroupMessageService : IEventGroupMessageService
    {
        private readonly ApplicationDbContext _context;
        public EventGroupMessageService(ApplicationDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<EventGroupMessages>> GetEventGroupMessagesAsync(int eventId)
        {
            if (eventId <= 0)
                return Enumerable.Empty<EventGroupMessages>();
            try
            {
                return await _context.EventGroupMessages
                    .Include(m => m.Sender)
                        .ThenInclude(s => s.Profile)
                    .Include(m => m.Event)
                    .Include(m => m.MessageReads)
                    .Where(m => m.EventId == eventId && !m.IsDeleted)
                    .OrderByDescending(m => m.IsPinned)
                    .ThenBy(m => m.SentAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve group messages for event {eventId}", ex);
            }
        }

        public async Task<EventGroupMessages> SendGroupMessageAsync(EventGroupMessages message)
        {
            if(message==null)
                throw new ArgumentNullException(nameof(message));
            if (string.IsNullOrWhiteSpace(message.Content))
                throw new ArgumentException("Message content is required");
            if(message.EventId <= 0)
            {
                throw new ArgumentException("Event ID is required");
            }
            try
            {
                //verify event exists
                var eventExists = await _context.Events
                    .AnyAsync(e => e.EventId == message.EventId && !e.IsDeleted && e.IsActive);
                if (!eventExists)
                    throw new ArgumentException("Event does not exist or is not active");
                //verify sender is an attendee or creator
                var isParticipant = await _context.EventAttendees
                    .AnyAsync(a => a.EventId == message.EventId &&
                                 a.UserId == message.SenderId &&
                                 a.Status == "accepted");
                var isCreator = await _context.Events
                    .AnyAsync(e => e.EventId == message.EventId && e.CreatorId == message.SenderId);
                if (!isParticipant && !isCreator)
                    throw new InvalidOperationException("User must be an attendee or creator to send messages");

                message.SentAt =DateTime.UtcNow;
                _context.EventGroupMessages.Add(message);
                await _context.SaveChangesAsync();

                //return with includes
                return await _context.EventGroupMessages
                    .Include(m => m.Sender)
                        .ThenInclude(s => s.Profile)
                    .Include(m => m.Event)
                    .FirstAsync(m => m.Id == message.Id);
            } 
            catch (Exception ex) when (!(ex is ArgumentException || ex is InvalidOperationException))
            {
                throw new InvalidOperationException("Failed to send group message", ex);
            }
        }

        public async Task<EventGroupMessages> SendAnnouncementAsync (int eventId, string senderId, string subject, string content)
        {
            if (eventId <= 0)
                throw new ArgumentException("Invalid event ID");

            if (string.IsNullOrWhiteSpace(senderId))
                throw new ArgumentException("Sender ID is required");

            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content is required");

            try
            {
                //verify sender is event creator
                var isCreator = await _context.Events
                    .AnyAsync(e => e.EventId == eventId && e.CreatorId == senderId);
                if (!isCreator)
                    throw new InvalidOperationException("Only event creators can send announcements");

                var announcement = new EventGroupMessages
                {
                    EventId = eventId,
                    SenderId = senderId,
                    Subject = subject,
                    Content = content,
                    MessageType = "announcement",
                    SentAt = DateTime.UtcNow,
                    IsPinned = true //announcements are auto-pinned
                };
                return await SendGroupMessageAsync(announcement);
            }
            catch (Exception ex) when (!(ex is ArgumentException || ex is InvalidOperationException))
            {
                throw new InvalidOperationException("Failed to send announcement", ex);
            }
        }

        public async Task<bool> DeleteMessageAsync(int messageId, string userId)
        {
            if (messageId <= 0 || string.IsNullOrWhiteSpace(userId))
                return false;
            try
            {
                var message = await _context.EventGroupMessages
                    .Include(m => m.Event)
                    .FirstOrDefaultAsync(m => m.Id == messageId);
                if (message == null || message.IsDeleted)
                    return false;

                //only sender or event creator can delete
                if (message.SenderId != userId && message.Event.CreatorId != userId)
                    return false;

                message.IsDeleted = true;
                await _context.SaveChangesAsync();

                return true;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Failed to delete message {messageId}", ex);
            }
        }

        public async Task<bool> PinMessageAsync(int messageId, string userId)
        {
            if (messageId <= 0 || string.IsNullOrWhiteSpace(userId))
                return false;
            try
            {
                var message = await _context.EventGroupMessages
                    .Include(m => m.Event)
                    .FirstOrDefaultAsync(m => m.Id == messageId);
                if (message == null || message.IsDeleted)
                    return false;

                //only event creator can pin
                if (message.Event.CreatorId != userId)
                    return false;

                message.IsPinned = true;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to pin message {messageId}", ex);
            }
        }

        public async Task<bool> UnpinMessageAsync(int messageId, string userId)
        {

            if (messageId <= 0 || string.IsNullOrWhiteSpace(userId))
                return false;
            try
            {
                var message = await _context.EventGroupMessages
                    .Include(m => m.Event)
                    .FirstOrDefaultAsync(m => m.Id == messageId);
                if (message == null || message.IsDeleted)
                    return false;

                //only event creator can unpin
                if (message.Event.CreatorId != userId)
                    return false;

                message.IsPinned = false;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to unpin message {messageId}", ex);
            }
        }
        
        public async Task<bool> MarkAsReadAsync(int messageId, string userId)
        {
            if (messageId <= 0 || string.IsNullOrWhiteSpace(userId))
                return false;
            try
            {
                var existingRead = await _context.EventGroupMessageReads
                    .AnyAsync(r => r.MessageId == messageId && r.UserId == userId);

                if (existingRead)
                    return true; //already marked as read
                var read = new EventGroupMessageReads
                {
                    MessageId = messageId,
                    UserId = userId,
                    ReadAt = DateTime.UtcNow
                };
                _context.EventGroupMessageReads.Add(read);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to mark message {messageId} as read", ex);
            }
        }

        public async Task<int> GetUnreadCountForEventAsync(int eventId, string userId)
        {
            if (eventId <= 0 || string.IsNullOrWhiteSpace(userId))
                return 0;
            try
            {
                var allMessages = await _context.EventGroupMessages
                    .Where(m => m.EventId == eventId && !m.IsDeleted && m.SenderId != userId)
                    .Select(m => m.Id)
                    .ToListAsync();
                var readMessages = await _context.EventGroupMessageReads
                    .Where(r => allMessages.Contains(r.MessageId) && r.UserId == userId)
                    .Select(r => r.MessageId)
                    .ToListAsync();
                return allMessages.Count - readMessages.Count;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get unread count for event {eventId}", ex);
            }
        }

        public async Task<IEnumerable<EventGroupMessages>> GetPinnedMessagesAsync(int eventId)
        {
            if (eventId <= 0) 
                return Enumerable.Empty<EventGroupMessages>();
            try
            {
                return await _context.EventGroupMessages
                    .Include(m => m.Sender)
                        .ThenInclude(s => s.Profile)
                    .Include(m => m.Event)
                    .Where(m => m.EventId == eventId && !m.IsDeleted && m.IsPinned)
                    .OrderByDescending(m => m.SentAt)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve pinned messages for event {eventId}", ex);
            }
        }
    }
}
