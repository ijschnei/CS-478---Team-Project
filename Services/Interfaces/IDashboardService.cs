using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<int> GetTotalEventsCountAsync(string? userId = null);
        Task<int> GetUpcomingEventsCountAsync(string? userId = null);
        Task<int> GetTotalAttendeesCountAsync(string? userId = null);
        Task<IEnumerable<Events>> GetRecentEventsAsync(string? userId = null, int count = 5);
        Task<Dictionary<string, int>> GetEventsByStatusAsync(string? userId = null);
        Task<Dictionary<string, int>> GetEventsByCategoryAsync(string? userId = null);
        Task<IEnumerable<Events>> GetPopularEventsAsync(int count = 10);
    }
}
