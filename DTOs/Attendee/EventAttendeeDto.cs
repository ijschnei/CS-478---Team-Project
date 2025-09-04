using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.DTOs.Attendee
{
    public class EventAttendeeDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string AttendeeType { get; set; } = string.Empty;
        public DateTime RSVP_Date { get; set; }
        public bool CheckedIn { get; set; }
        public DateTime? CheckInTime { get; set; }
        public string? Notes { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
