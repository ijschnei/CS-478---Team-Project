using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Interfaces
{
    public interface ICustomFieldsService
    {
        Task<IEnumerable<EventCustomFields>> GetEventCustomFieldsAsync(int eventId);
        Task<EventCustomFields> CreateCustomFieldAsync(EventCustomFields customField);
        Task<EventCustomFields?> UpdateCustomFieldAsync(EventCustomFields customField);
        Task<bool> DeleteCustomFieldAsync(int id);
        Task<bool> UpdateCustomFieldValueAsync(int fieldId, string value);
    }
}
