using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Implementation
{
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

        public async Task<Events?> GetEventByIdAsync(int id)
        {
            return await _context.Events
                .Include(e => e.Creator)
                    .ThenInclude(c => c.Profile)
                .Include(e => e.Theme)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Attendees)
                    .ThenInclude(a => a.User)
                        .ThenInclude(u => u.Profile)
                .Include(e => e.CustomFields)
                .FirstOrDefaultAsync(e => e.EventId == id && !e.IsDeleted);
        }
        public async Task<IEnumerable<Events>> GetEventsByUserIdAsync(string userId)
        {
            return await _context.Events
                .Include(e => e.Creator)
                .Include(e => e.Theme)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Attendees)
                .Where(e => e.CreatorId == userId && !e.IsDeleted && e.IsActive)
                .OrderBy(e => e.StartDateTime)
                .ToListAsync();
        }
        public async Task<IEnumerable<Events>> GetUpcomingEventsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Events
                .Include(e => e.Creator)
                .Include(e => e.Theme)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Attendees)
                .Where(e => e.StartDateTime > now && !e.IsDeleted && e.IsActive)
                .OrderBy(e => e.StartDateTime)
                .Take(10)
                .ToListAsync();
        }
        public async Task<Events> CreateEventAsync(Events eventModel)
        {
            eventModel.CreatedAt = DateTime.UtcNow;
            eventModel.UpdatedAt = DateTime.UtcNow;
            if (eventModel.EndDateTime.HasValue && eventModel.EndDateTime <= eventModel.StartDateTime)
            {
                throw new ArgumentException("End date must be after start date");
            }
            _context.Events.Add(eventModel);
            await _context.SaveChangesAsync();

            //add creator as organizer
            await AddAttendeeAsync(eventModel.EventId, eventModel.CreatorId, "organizer");
            return eventModel;
        }
        public async Task<Events?> UpdateEventAsync(Events eventModel)
        {
           
            var existingEvent = await _context.Events.FindAsync(eventModel.EventId);
            if (existingEvent == null) return null;
            //CSS validation to prevent harmful script injection
            if (!string.IsNullOrEmpty(eventModel.CustomCss))
            {
                if (!ValidateCustomCss(eventModel.CustomCss))
                {
                    throw new ArgumentException("CSS contains potentially unsafe content");
                }
            }
            //Update properties
            existingEvent.EventName = eventModel.EventName;
            existingEvent.EventDescription = eventModel.EventDescription;
            existingEvent.EventDetails = eventModel.EventDetails;
            existingEvent.StartDateTime = eventModel.StartDateTime;
            existingEvent.EndDateTime = eventModel.EndDateTime;
            existingEvent.TimeZone = eventModel.TimeZone;
            existingEvent.IsAllDay = eventModel.IsAllDay;
            existingEvent.VenueName = eventModel.VenueName;
            existingEvent.Address = eventModel.Address;
            existingEvent.City = eventModel.City;
            existingEvent.State = eventModel.State;
            existingEvent.Country = eventModel.Country;
            existingEvent.PostalCode = eventModel.PostalCode;
            existingEvent.IsVirtual = eventModel.IsVirtual;
            existingEvent.VirtualMeetingUrl = eventModel.VirtualMeetingUrl;
            existingEvent.MaxAttendees = eventModel.MaxAttendees;
            existingEvent.IsPrivate = eventModel.IsPrivate;
            existingEvent.RequiresApproval = eventModel.RequiresApproval;
            existingEvent.AllowGuestList = eventModel.AllowGuestList;
            existingEvent.ThemeId = eventModel.ThemeId;
            existingEvent.CustomCss = eventModel.CustomCss;
            existingEvent.BannerImageUrl = eventModel.BannerImageUrl;
            existingEvent.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingEvent;
        }
        //helper method for checking potentially dangerous script
        private static bool ValidateCustomCss(string css)
        {
            var dangerousPatterns = new[]
            {
                "javascript:",
                "expression(",
                "import",
                "@import",
                "url(data:",
                "<script",
                "onclick",
                "onerror",
                "onload"
            };
            var cssLower = css.ToLower();
            return !dangerousPatterns.Any(pattern=>cssLower.Contains(pattern));
        }
        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete == null) return false;
            eventToDelete.IsDeleted = true;
            eventToDelete.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddAttendeeAsync(int eventId, string userId, string attendeeType = "attendee")
        {
            //check if already attending
            var existingAttendee = await _context.EventAttendees
                .FirstOrDefaultAsync(ea => ea.EventId == eventId && ea.UserId == userId);
            if (existingAttendee != null) return false;
            //get event to check RequiresApproval setting
            var eventItem = await _context.Events
                .FirstOrDefaultAsync(e => e.EventId == eventId && !e.IsDeleted && e.IsActive);
            if (eventItem == null) return false;
            //determine initial status based on event settings
            string initialStatus;
            if(attendeeType == "organizer" || attendeeType == "co-organizer")
            {
                initialStatus = "accepted"; //organizers always accepted
            }
            else if (eventItem.RequiresApproval)
            {
                initialStatus = "pending"; //needs approval from organizer
            }
            else
            {
                initialStatus = "accepted"; //auto-accept if no approval required
            }
                var eventAttendee = new EventAttendees
                {
                    EventId = eventId,
                    UserId = userId,
                    AttendeeType = attendeeType,
                    Status = initialStatus,
                    RSVP_Date = DateTime.UtcNow
                };

            _context.EventAttendees.Add(eventAttendee);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAttendeeStatusAsync(int eventId, string userId, string status)
        {
            var attendee = await _context.EventAttendees
                .FirstOrDefaultAsync(ea => ea.EventId == eventId && ea.UserId == userId);
            if (attendee == null) return false;
            attendee.Status = status;
            attendee.RSVP_Date = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveAttendeeAsync(int eventId, string userId)
        {
            var attendee = await _context.EventAttendees
                .FirstOrDefaultAsync(ea => ea.EventId == eventId && ea.UserId == userId);
            if (attendee == null) return false;

            _context.EventAttendees.Remove(attendee);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<EventAttendees>> GetEventAttendeesAsync(int eventId)
        {
            return await _context.EventAttendees
                .Include(ea => ea.User)
                    .ThenInclude(u => u.Profile)
                .Where(ea => ea.EventId == eventId)
                .OrderBy(ea => ea.AttendeeType)
                .ThenBy(ea => ea.RSVP_Date)
                .ToListAsync();
        }
        public async Task<IEnumerable<Events>> SearchEventsAsync(string searchTerm, int? categoryId = null)
        {
            var query = _context.Events
                .Include(e => e.Creator)
                .Include(e => e.Theme)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Attendees)
                .Where(e => !e.IsDeleted && e.IsActive);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(e =>
                e.EventName.Contains(searchTerm) ||
                e.EventDescription!.Contains(searchTerm) ||
                e.City!.Contains(searchTerm) ||
                e.VenueName!.Contains(searchTerm));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(e => e.Categories.Any(c => c.CategoryId == categoryId.Value));
            }

            return await query
                .OrderBy(e => e.StartDateTime)
                .ToListAsync();
        }
    }
}
