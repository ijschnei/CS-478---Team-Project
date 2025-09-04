using System.ComponentModel.DataAnnotations;
using CS478_EventPlannerProject.DTOs.Attendee;
using CS478_EventPlannerProject.DTOs.Custom;
namespace CS478_EventPlannerProject.DTOs.Event
{
    public class EventDetailsDto : EventSummaryDto
    {
        public string? EventDetails { get; set; }
        public string? TimeZone { get; set; }
        public bool IsAllDay { get; set; }
        public string? VenueName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? VirtualMeetingUrl { get; set; }
        public string CreatorId { get; set; } = string.Empty;
        public int? MaxAttendess { get; set; }
        public bool RequiresApproval { get; set; }
        public bool AllowGuestList { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<EventAttendeeDto> Attendees { get; set; } = new List<EventAttendeeDto>();
        public List<EventCustomFieldDto> CustomFields { get; set; } = new List<EventCustomFieldDto>();
    }
}
