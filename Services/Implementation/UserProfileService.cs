using AutoMapper;
using CS478_EventPlannerProject.Data;
using CS478_EventPlannerProject.Models;
using CS478_EventPlannerProject.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CS478_EventPlannerProject.Services.Implementation
{
    public class UserProfileService : IUserProfileService
    {
        private readonly ApplicationDbContext _context;
        public UserProfileService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<UserProfiles?> GetProfileByUserIdAsync(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                return null;
            try
            {
                return await _context.UserProfiles
                    .Include(up => up.User)//include related user
                    .FirstOrDefaultAsync(up => up.UserId == userId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to retrieve profile for user {userId}", ex);
            }
        }

        public async Task<UserProfiles> CreateProfileAsync(UserProfiles profile)
        {
            if (profile == null)
                throw new ArgumentNullException(nameof(profile));
            if (string.IsNullOrWhiteSpace(profile.UserId))
                throw new ArgumentException("UserId is required");
            try
            {
                //check if profile already exists
                var existingProfile = await GetProfileByUserIdAsync(profile.UserId);
                if (existingProfile != null)
                    throw new InvalidOperationException("Profile already exists for this user");
                //audit fields
                profile.CreatedAt = DateTime.UtcNow;
                profile.UpdatedAt = DateTime.UtcNow;
                _context.UserProfiles.Add(profile);
                await _context.SaveChangesAsync();
                return profile;
            }
            catch (Exception ex) when (!(ex is ArgumentException || ex is InvalidOperationException))
            {
                throw new InvalidOperationException("Failed to create user profile", ex);
            }
        }

        public async Task<UserProfiles?> UpdateProfileAsync(UserProfiles profile)
        {
            if (profile == null) return null;
            try
            {
                var existingProfile = await _context.UserProfiles
                    .FirstOrDefaultAsync(up => up.UserProfileId == profile.UserProfileId);
                if (existingProfile == null) return null;

                //update fields
                existingProfile.FirstName = profile.FirstName;
                existingProfile.LastName = profile.LastName;
                existingProfile.DisplayName = profile.DisplayName;
                existingProfile.Bio = profile.Bio;
                existingProfile.ProfileImageUrl = profile.ProfileImageUrl;
                existingProfile.DateOfBirth = profile.DateOfBirth;
                existingProfile.Location = profile.Location;
                existingProfile.Website = profile.Website;
                existingProfile.FacebookUrl = profile.FacebookUrl;
                existingProfile.TwitterUrl = profile.TwitterUrl;
                existingProfile.LinkedInUrl = profile.LinkedInUrl;
                existingProfile.IsPublic = profile.IsPublic;
                existingProfile.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return existingProfile;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to update profile {profile.UserProfileId}", ex);
            }
        }
        public async Task<bool> DeleteProfileAsync(string userId)
        {
            if(string.IsNullOrWhiteSpace(userId)) return false;
            try
            {
                var profile = await _context.UserProfiles
                    .FirstOrDefaultAsync(up => up.UserId == userId);
                if (profile == null) return false;
                _context.UserProfiles.Remove(profile);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to delete profile for user {userId}", ex);
            }
        }
    }

}
