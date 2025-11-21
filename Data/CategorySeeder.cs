using CS478_EventPlannerProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CS478_EventPlannerProject.Data
{
    public static class CategorySeeder
    {
        public static async Task SeedCategoriesAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Check if categories already exist
                if (await context.EventCategories.AnyAsync())
                {
                    Console.WriteLine("Categories already seeded.");
                    return;
                }

                var categories = new List<EventCategory>
                {
                    new EventCategory
                    {
                        Name = "Technology",
                        Description = "Tech conferences, workshops, and meetups",
                        ColorCode = "#3498db",
                        IconUrl = "bi-laptop",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Business",
                        Description = "Business networking, conferences, and seminars",
                        ColorCode = "#2ecc71",
                        IconUrl = "bi-briefcase",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Education",
                        Description = "Educational workshops, training, and classes",
                        ColorCode = "#f39c12",
                        IconUrl = "bi-book",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Social",
                        Description = "Social gatherings, parties, and celebrations",
                        ColorCode = "#e74c3c",
                        IconUrl = "bi-people",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Sports & Fitness",
                        Description = "Sports events, fitness classes, and outdoor activities",
                        ColorCode = "#1abc9c",
                        IconUrl = "bi-trophy",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Arts & Culture",
                        Description = "Art exhibitions, cultural events, and performances",
                        ColorCode = "#9b59b6",
                        IconUrl = "bi-palette",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Food & Drink",
                        Description = "Culinary events, tastings, and food festivals",
                        ColorCode = "#e67e22",
                        IconUrl = "bi-cup-hot",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Health & Wellness",
                        Description = "Wellness workshops, health seminars, and yoga classes",
                        ColorCode = "#16a085",
                        IconUrl = "bi-heart-pulse",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Music",
                        Description = "Concerts, music festivals, and performances",
                        ColorCode = "#c0392b",
                        IconUrl = "bi-music-note",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Networking",
                        Description = "Professional networking events and mixers",
                        ColorCode = "#34495e",
                        IconUrl = "bi-diagram-3",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Charity & Causes",
                        Description = "Fundraisers, charity events, and volunteer activities",
                        ColorCode = "#d35400",
                        IconUrl = "bi-heart",
                        IsActive = true
                    },
                    new EventCategory
                    {
                        Name = "Community",
                        Description = "Community gatherings, town halls, and local events",
                        ColorCode = "#27ae60",
                        IconUrl = "bi-house-door",
                        IsActive = true
                    }
                };

                context.EventCategories.AddRange(categories);
                await context.SaveChangesAsync();

                Console.WriteLine($"Seeded {categories.Count} categories successfully.");
            }
        }
    }
}