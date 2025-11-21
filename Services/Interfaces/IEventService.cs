using CS478_EventPlannerProject.Models;
namespace CS478_EventPlannerProject.Services.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<Events>> GetAllEventsAsync();
        Task<Events?> GetEventByIdAsync(int id);
        Task<IEnumerable<Events>> GetEventsByUserIdAsync(string userId);
        Task<IEnumerable<Events>> GetUpcomingEventsAsync();
        Task<Events> CreateEventAsync(Events eventModel);
        Task<Events?> UpdateEventAsync(Events eventModel);
        Task<bool> DeleteEventAsync(int id);
        Task<bool> AddAttendeeAsync(int eventId, string userId, string attendeeType = "attendee");
        Task<bool> UpdateAttendeeStatusAsync(int eventId, string userId, string status);
        Task<bool> RemoveAttendeeAsync(int eventId, string userId);
        Task<IEnumerable<EventAttendees>> GetEventAttendeesAsync(int eventId);
        Task<IEnumerable<Events>> SearchEventsAsync(
         string? searchTerm = null,
         int? categoryId = null,
         string? location = null,
         string? eventType = null,
         string? dateRange = null,
         DateTime? startDate = null,
        DateTime? endDate = null);
    }


}
