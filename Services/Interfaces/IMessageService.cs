using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<Messages>> GetConversationAsync(Guid conversationId);
        Task<IEnumerable<Messages>> GetUserMessagesAsync(string userId);
        Task<Messages> SendMessageAsync(Messages message);
        Task<bool> MarkAsReadAsync(int messageId, string userId);
        Task<int> GetUnreadCountAsync(string userId);
        Task<IEnumerable<Messages>> GetEventMessagesAsync(int eventId);
        Task<Messages> SendEventInvitationAsync(int eventId, string senderId, string receiverId, string? customMessage = null);
        Task<bool> AcceptEventInvitationAsync(int messageId, string userId);
    }
}
