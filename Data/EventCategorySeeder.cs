using CS478_EventPlannerProject.Models;
using Microsoft.EntityFrameworkCore;

namespace CS478_EventPlannerProject.Data
{
    public static class EventCategorySeeder
    {
        public static async Task AssignCategoriesToEventsAsync(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Check if mappings already exist
                if (await context.EventCategoryMappings.AnyAsync())
                {
                    Console.WriteLine("Event categories already assigned.");
                    return;
                }

                // Get categories
                var techCategory = await context.EventCategories.FirstOrDefaultAsync(c => c.Name == "Technology");
                var businessCategory = await context.EventCategories.FirstOrDefaultAsync(c => c.Name == "Business");
                var educationCategory = await context.EventCategories.FirstOrDefaultAsync(c => c.Name == "Education");
                var socialCategory = await context.EventCategories.FirstOrDefaultAsync(c => c.Name == "Social");
                var networkingCategory = await context.EventCategories.FirstOrDefaultAsync(c => c.Name == "Networking");
                var communityCategory = await context.EventCategories.FirstOrDefaultAsync(c => c.Name == "Community");

                // Get events by name
                var techSummit = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Tech Innovation Summit 2025");
                var masterclass = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Event Planning Masterclass");
                var networking = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Professional Networking Mixer");
                var privateMeeting = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Private Community Gathering");
                var pitchNight = await context.Events.FirstOrDefaultAsync(e => e.EventName == "Startup Pitch Night");

                var mappings = new List<EventCategoryMapping>();

                // Assign categories to specific events
                if (techSummit != null && techCategory != null)
                {
                    mappings.Add(new EventCategoryMapping { EventId = techSummit.EventId, CategoryId = techCategory.Id });
                    if (businessCategory != null)
                        mappings.Add(new EventCategoryMapping { EventId = techSummit.EventId, CategoryId = businessCategory.Id });
                }

                if (masterclass != null && educationCategory != null)
                {
                    mappings.Add(new EventCategoryMapping { EventId = masterclass.EventId, CategoryId = educationCategory.Id });
                    if (businessCategory != null)
                        mappings.Add(new EventCategoryMapping { EventId = masterclass.EventId, CategoryId = businessCategory.Id });
                }

                if (networking != null && networkingCategory != null)
                {
                    mappings.Add(new EventCategoryMapping { EventId = networking.EventId, CategoryId = networkingCategory.Id });
                    if (socialCategory != null)
                        mappings.Add(new EventCategoryMapping { EventId = networking.EventId, CategoryId = socialCategory.Id });
                }

                if (privateMeeting != null && communityCategory != null)
                {
                    mappings.Add(new EventCategoryMapping { EventId = privateMeeting.EventId, CategoryId = communityCategory.Id });
                    if (socialCategory != null)
                        mappings.Add(new EventCategoryMapping { EventId = privateMeeting.EventId, CategoryId = socialCategory.Id });
                }

                if (pitchNight != null && businessCategory != null)
                {
                    mappings.Add(new EventCategoryMapping { EventId = pitchNight.EventId, CategoryId = businessCategory.Id });
                    if (techCategory != null)
                        mappings.Add(new EventCategoryMapping { EventId = pitchNight.EventId, CategoryId = techCategory.Id });
                    if (networkingCategory != null)
                        mappings.Add(new EventCategoryMapping { EventId = pitchNight.EventId, CategoryId = networkingCategory.Id });
                }

                // Assign random categories to remaining events
                var allEvents = await context.Events.Where(e => !e.IsDeleted && e.IsActive).ToListAsync();
                var allCategories = await context.EventCategories.Where(c => c.IsActive).ToListAsync();

                foreach (var evt in allEvents)
                {
                    var hasCategories = mappings.Any(m => m.EventId == evt.EventId);
                    if (!hasCategories && allCategories.Any())
                    {
                        var random = new Random();
                        var numCategories = random.Next(1, 3);
                        var selectedCategories = allCategories.OrderBy(x => random.Next()).Take(numCategories);

                        foreach (var category in selectedCategories)
                        {
                            mappings.Add(new EventCategoryMapping
                            {
                                EventId = evt.EventId,
                                CategoryId = category.Id
                            });
                        }
                    }
                }

                if (mappings.Any())
                {
                    context.EventCategoryMappings.AddRange(mappings);
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Assigned categories to {mappings.Count} event-category mappings.");
                }
            }
        }
    }
}