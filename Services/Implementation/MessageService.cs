using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Implementation
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        public MessageService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<IEnumerable<Messages>> GetConversationAsync(Guid conversationId)
        {
            if (conversationId == Guid.Empty)
                return Enumerable.Empty<Messages>();
            try
            {
                return await _context.Messages
                    .Include(m => m.Sender)
                        .ThenInclude(s => s.Profile)
                    .Include(m => m.Receiver)
                        .ThenInclude(r => r.Profile)
                    .Include(m => m.RelatedEvent)
                    .Where(m => m.ConversationId == conversationId && !m.IsDeleted)
                    .OrderBy(m => m.SentAt)
                    .ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"Failed to retrieve conversation {conversationId}", ex);
            }
        }
        public async Task<IEnumerable<Messages>> GetUserMessagesAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return Enumerable.Empty<Messages>();
            try
            {
                return await _context.Messages
                    .Include(m => m.Sender)
                        .ThenInclude(s => s.Profile)
                    .Include(m => m.Receiver)
                        .ThenInclude(r => r.Profile)
                    .Include(m => m.RelatedEvent)
                    .Where(m => (m.SenderId == userId && !m.DeletedBySender) || (m.ReceiverId == userId && !m.DeletedByReceiver))
                    .Where(m => !m.IsDeleted)
                    .OrderByDescending(m => m.SentAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve messages for user {userId}", ex);
            }
        }
        public async Task<Messages> SendMessageAsync(Messages message)
        {
            if (message == null)
                throw new ArgumentNullException(nameof(message));
            //validation
            if (string.IsNullOrWhiteSpace(message.SenderId))
                throw new ArgumentException("SenderId is required");
            if (string.IsNullOrWhiteSpace(message.ReceiverId))
                throw new ArgumentException("ReceiverId is required");
            if (string.IsNullOrWhiteSpace(message.Content))
                throw new ArgumentException("Content is required");
            if (message.SenderId == message.ReceiverId)
                throw new ArgumentException("Cannot send message to yourself");
            try
            {
                //generate conversation id if not provided
                if (message.ConversationId == Guid.Empty)
                {
                    //check if there's an existing conversation between these users
                    var existingConversation = await _context.Messages
                        .Where(m => (m.SenderId == message.SenderId && m.ReceiverId == message.ReceiverId) ||
                        (m.SenderId == message.ReceiverId && m.ReceiverId == message.SenderId))
                        .Select(m => m.ConversationId)
                        .FirstOrDefaultAsync();
                    message.ConversationId = existingConversation != Guid.Empty ?
                        existingConversation : Guid.NewGuid();
                }
                //set timestamp
                message.SentAt = DateTime.UtcNow;

                //validate messsage type
                var validMessageTypes = new[] { "direct", "event_related", "system" };
                if (!validMessageTypes.Contains(message.MessageType))
                {
                    message.MessageType = "direct";
                }
                //if its event-related, verify the event exists
                if (message.MessageType == "event_related" && message.RelatedEventId.HasValue)
                {
                    var eventExists = await _context.Events
                        .AnyAsync(e => e.EventId == message.RelatedEventId.Value && !e.IsDeleted);
                    if (!eventExists)
                    {
                        throw new ArgumentException("Related event does not exist");
                    }
                }
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                //return message with included related data
                return await _context.Messages
                    .Include(m => m.Sender)
                        .ThenInclude(s => s.Profile)
                    .Include(m => m.Receiver)
                        .ThenInclude(r => r.Profile)
                    .Include(m => m.RelatedEvent)
                    .FirstAsync(m => m.Id == message.Id);
            }
            catch (Exception ex) when (!(ex is ArgumentException))
            {
                throw new InvalidOperationException("Failed to send message", ex);
            }
        }
        public async Task<bool> MarkAsReadAsync(int messageId, string userId)
        {
            if (messageId <= 0 || string.IsNullOrWhiteSpace(userId))
                return false;
            try
            {
                var message = await _context.Messages
                    .FirstOrDefaultAsync(m=>m.Id==messageId&&m.ReceiverId==userId);
                if (message == null || message.IsRead)
                    return false;
                message.IsRead = true;
                message.ReadAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"Failed to mark message {messageId} as read", ex);
            }
        }
        public async Task<int> GetUnreadCountAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return 0;
            try 
            {
                return await _context.Messages
                    .CountAsync(m => m.ReceiverId == userId &&
                    !m.IsRead &&
                    !m.DeletedByReceiver &&
                    !m.IsDeleted);
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"Failed to get unread count for user {userId}", ex);

            }
        }

        public async Task<IEnumerable<Messages>> GetEventMessagesAsync(int eventId)
        {
            if (eventId <= 0)
                return Enumerable.Empty<Messages>();
            try
            {
                return await _context.Messages
                    .Include(m => m.Sender)
                        .ThenInclude(s => s.Profile)
                    .Include(m => m.Receiver)
                        .ThenInclude(r => r.Profile)
                    .Where(m => m.RelatedEventId == eventId && !m.IsDeleted)
                    .OrderBy(m => m.SentAt)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve messages for event {eventId}", ex);
            }
        }
    }
}
