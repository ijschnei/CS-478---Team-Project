using AutoMapper;
using CS478_EventPlannerProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CS478_EventPlannerProject.Data
{
    public static class UserSeeder
    {
        public static async Task SeedDummyUsersAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                //dummy user definitions
                var dummyUsers = new List<(string email, string username, string firstName, string lastName, string displayName, string bio, string location, bool isPublic)>
                {
                    ("alice.smith@example.com", "alice_smith", "Alice", "Smith", "AliceS", "Event enthusiast and organizer", "New York, NY", true),
                    ("bob.jones@example.com", "bob_jones", "Bob", "Jones", "BobJ", "Tech conference speaker", "San Francisco, CA", true),
                    ("carol.williams@example.com", "carol_williams", "Carol", "Williams", "CarolW", "Professional event planner", "Chicago, IL", true),
                    ("david.brown@example.com", "david_brown", "David", "Brown", "DavidB", "Community organizer", "Austin, TX", false), // Private profile
                    ("emma.davis@example.com", "emma_davis", "Emma", "Davis", "EmmaD", "Networking enthusiast and entrepreneur", "Boston, MA", true)
                };
                foreach (var(email, username, firstName, lastName, displayName, bio, location, isPublic) in dummyUsers)
                {
                    //check if user already exists
                    var existingUser = await userManager.FindByEmailAsync(email);
                    if(existingUser != null)
                    {
                        continue; //user already exists skip
                    }
                    //create newUser = new Users
                    var newUser = new Users
                    {
                        UserName = username,
                        Email = email,
                        EmailConfirmed = true,
                        IsActive = true,
                        CreatedAt = DateTime.UtcNow
                    };
                    var result = await userManager.CreateAsync(newUser, "DummyPassword123!");
                    if (!result.Succeeded)
                    {
                        continue;
                    }
                    //create user profile
                    var userProfile = new UserProfiles
                    {
                        UserId = newUser.Id,
                        FirstName = firstName,
                        DisplayName = displayName,
                        Bio = bio,
                        Location = location,
                        IsPublic = isPublic,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    context.UserProfiles.Add(userProfile);
                }
                await context.SaveChangesAsync();
            }
        }
    }
}
