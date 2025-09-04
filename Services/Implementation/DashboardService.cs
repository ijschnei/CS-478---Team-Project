using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Implementation
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        public DashboardService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<int> GetTotalEventsCountAsync(string? userId = null)
        {
            try
            {
                var query = _context.Events.Where(e => !e.IsDeleted && e.IsActive);
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    query = query.Where(e => e.CreatorId == userId);
                }
                return await query.CountAsync();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Failed to get total events count", ex);
            }
        }

        public async Task<int> GetUpcomingEventsCountAsync(string? userId = null)
        {
            var now = DateTime.UtcNow;
            try
            {
                var query = _context.Events
                    .Where(e => !e.IsDeleted && e.IsActive && e.StartDateTime > now);
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    query = query.Where(e => e.CreatorId == userId);
                }
                return await query.CountAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get upcoming events count", ex);
            }
        }
        public async Task<int> GetTotalAttendeesCountAsync(string? userId = null)
        {
            try
            {
                var query = _context.EventAttendees
                    .Include(ea => ea.Event)
                    .Where(ea => ea.Status == "accepted" &&
                    !ea.Event.IsDeleted &&
                    ea.Event.IsActive);
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    query = query.Where(ea => ea.Event.CreatorId == userId);
                }
                return await query.CountAsync();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Failed to get total attendees count", ex);
            }

        }
        public async Task<IEnumerable<Events>> GetRecentEventsAsync(string? userId = null, int count = 5)
        {
            try
            {
                var query = _context.Events
                    .Include(e => e.Creator)
                        .ThenInclude(c => c.Profile)
                    .Include(e => e.Theme)
                    .Include(e => e.Categories)
                        .ThenInclude(c => c.Category)
                    .Include(e => e.Attendees.Where(a => a.Status == "accepted"))
                    .Where(e => !e.IsDeleted && e.IsActive);
                if (!string.IsNullOrWhiteSpace(userId)) 
                {
                    query = query.Where(e => e.CreatorId == userId);
                }
                return await query
                    .OrderByDescending(e => e.CreatedAt)
                    .Take(count)
                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Failed to get recent events", ex);
            }

        }
        public async Task<Dictionary<string, int>> GetEventsByStatusAsync(string? userId = null)
        {
            var now = DateTime.UtcNow;
            try
            {
                var query = _context.Events.Where(e => !e.IsDeleted && e.IsActive);
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    query = query.Where(e => e.CreatorId == userId);
                }
                var events = await query.ToListAsync();
                var result = new Dictionary<string, int>
                {
                    ["Upcoming"] = events.Count(e => e.StartDateTime > now),
                    ["Ongoing"] = events.Count(e => e.StartDateTime <= now &&
                                  (e.EndDateTime == null || e.EndDateTime > now)),
                    ["Past"] = events.Count(e => e.EndDateTime?.CompareTo(now) < 0 ||
                                   (e.EndDateTime == null && e.StartDateTime < now))
                };
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get events by status", ex);
            }

        }
        public async Task<Dictionary<string, int>> GetEventsByCategoryAsync(string? userId = null)
        {
            try
            {
                var query = _context.EventCategoryMappings
                    .Include(ecm => ecm.Event)
                    .Include(ecm => ecm.Category)
                    .Where(ecm => !ecm.Event.IsDeleted && ecm.Event.IsActive && ecm.Category.IsActive);
                if (!string.IsNullOrWhiteSpace(userId))
                {
                    query = query.Where(ecm=>ecm.Event.CreatorId==userId);
                }
                var categoryMappings = await query.ToListAsync();
                var result = categoryMappings
                    .GroupBy(ecm => ecm.Category.Name)
                    .ToDictionary(g => g.Key, g => g.Count());
                //add "uncategorized for events without categories
                var totalEvents = await GetTotalEventsCountAsync(userId);
                var categorizedEvents = result.Values.Sum();
                var uncategorizedCount = totalEvents - categorizedEvents;
                if (uncategorizedCount > 0)
                {
                    result["Uncategorized"] = uncategorizedCount;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get events by category", ex);
            }

        }
        public async Task<IEnumerable<Events>> GetPopularEventsAsync(int count = 10)
        {
            try
            {
                return await _context.Events
                    .Include(e => e.Creator)
                        .ThenInclude(c=>c.Profile)
                    .Include(e=>e.Theme)
                    .Include(e=>e.Categories)
                        .ThenInclude(c=>c.Category)
                    .Include(e=>e.Attendees.Where(a=>a.Status=="accepted"))
                    .Where(e=>!e.IsDeleted && e.IsActive)
                    .OrderByDescending(e=>e.Attendees.Count(a=>a.Status=="accepted"))
                    .ThenByDescending(e=>e.CreatedAt)
                    .Take(count)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to get popular events", ex);
            }

        }
    }

    
}
