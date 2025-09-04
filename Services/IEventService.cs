using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services
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
        Task<bool?> UpdateAttendeeAsync(int eventId, string userId, string status);
        Task<bool> RemoveAttendeeAsync(int eventId, string userId);
        Task<IEnumerable<EventAttendees>> GetEventAttendeesAsync(int eventId);
        Task<IEnumerable<Events>> SearchEventsAsync(string searchTerm, int? categoryId = null);
    }

    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _context;
        public EventService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Events>> GetAllEventsAsync()
        {
            return await _context.Events
                .Include(e => e.Creator)
                .Include(e => e.Theme)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Attendees)
                .Where(e => !e.IsDeleted && e.IsActive)
                .OrderBy(e => e.StartDateTime)
                .ToListAsync();
        }

        public
    }
}
