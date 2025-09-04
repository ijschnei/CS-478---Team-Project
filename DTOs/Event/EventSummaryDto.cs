using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.DTOs.Event
{
    public class EventSummaryDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string? EventDescription { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string FormattedLocation { get; set; } = string.Empty;
        public bool IsVirtual { get; set; }
        public bool IsPrivate { get; set; }
        public int AttendeeCount { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public string? BannerImageUrl { get; set; }
        public List<string> Categories { get; set; } = new List<string>();
        public string Status { get; set; } = string.Empty; // Upcoming, Ongoing, Past
    }
}
