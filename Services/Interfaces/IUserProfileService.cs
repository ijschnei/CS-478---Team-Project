using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfiles?> GetProfileByUserIdAsync(string userId);
        Task<UserProfiles> CreateProfileAsync(UserProfiles profile);
        Task<UserProfiles?> UpdateProfileAsync(UserProfiles profile);
        Task<bool> DeleteProfileAsync(string userId);
        Task<IEnumerable<UserProfiles>> SearchProfilesAsync(string searchTerm);
        Task<IEnumerable<UserProfiles>> GetAllPublicProfilesAsync();
    }
}
