using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<EventCategory>> GetAllCategoriesAsync();
        Task<EventCategory?> GetCategoryByIdAsync(int id);
        Task<EventCategory> CreateCategoryAsync(EventCategory category);
        Task<EventCategory?> UpdateCategoryAsync(EventCategory category);
        Task<bool> DeleteCategoryAsync(int id);
        Task<bool> AssignCategoryToEventAsync(int eventId, int categoryId);
        Task<bool> RemoveCategoryFromEventAsync(int eventId, int categoryId);
    }
}
