using CS478_EventPlannerProject.Data;
using Microsoft.EntityFrameworkCore;

namespace CS478_EventPlannerProject.Services
{
    public class EventCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<EventCleanupService> _logger;

        public EventCleanupService(IServiceProvider serviceProvider, ILogger<EventCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await MarkPastEventsAsInactive();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error marking past events as inactive");
                }

                // Run every 24 hours
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task MarkPastEventsAsInactive()
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>(); // Replace with your DbContext name

            var pastEvents = await context.Events
                .Where(e => e.EndDateTime.HasValue && e.EndDateTime.Value < DateTime.UtcNow && e.IsActive)
                .ToListAsync();

            foreach (var evt in pastEvents)
            {
                evt.IsActive = false;
                evt.UpdatedAt = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();
            _logger.LogInformation($"Marked {pastEvents.Count} past events as inactive");
        }
    }
}