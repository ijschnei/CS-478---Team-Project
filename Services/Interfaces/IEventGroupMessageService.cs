using CS478_EventPlannerProject.Models;

namespace CS478_EventPlannerProject.Services.Interfaces
{
    public interface IEventGroupMessageService
    {
        Task<IEnumerable<EventGroupMessages>> GetEventGroupMessagesAsync(int eventId);
        Task<EventGroupMessages> SendGroupMessageAsync(EventGroupMessages message);
        Task<EventGroupMessages> SendAnnouncementAsync(int eventId, string senderId, string subject, string content);
        Task<bool> DeleteMessageAsync(int messageId, string userId);
        Task<bool> PinMessageAsync(int messageId, string userId);
        Task<bool> UnpinMessageAsync(int messageId, string userId);
        Task<bool> MarkAsReadAsync(int messageId, string userId);
        Task<int> GetUnreadCountForEventAsync(int eventId, string userId);
        Task<IEnumerable<EventGroupMessages>> GetPinnedMessagesAsync(int eventId);
    }
}
