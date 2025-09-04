using CS478_EventPlannerProject.DTOs.Event;

namespace CS478_EventPlannerProject.DTOs.Common
{
    public class DashboardStatsDto
    {
        public int TotalEvents { get; set; }
        public int UpcomingEvents { get; set; }
        public int TotalAttendees { get; set; }
        public int UnreadMessages { get; set; }
        public List<EventSummaryDto> RecentEvents { get; set; } = new List<EventSummaryDto>();
        public Dictionary<string, int> EventsByStatus { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> EventsByCategory { get; set; } = new Dictionary<string, int>();
    }
}
