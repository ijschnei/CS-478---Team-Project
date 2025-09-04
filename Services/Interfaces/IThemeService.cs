using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Interfaces
{
    public interface IThemeService
    {
        Task<IEnumerable<EventTheme>> GetAllThemesAsync();
        Task<IEnumerable<EventTheme>> GetActiveThemesAsync();
        Task<EventTheme?> GetThemeByIdAsync(int id);
        Task<EventTheme> CreateThemeAsync(EventTheme theme);
        Task<EventTheme?> UpdateThemeAsync(EventTheme theme);
        Task<bool> DeleteThemeAsync(int id);
    }
}
