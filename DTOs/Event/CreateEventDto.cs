using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.DTOs.Event
{
    public class CreateEventDto
    {
        [Required]
        [MaxLength(200)]
        public string EventName { get; set; } = string.Empty;

        public string? EventDescription { get; set; }

        [MaxLength(500)]
        public string? EventDetails { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        [MaxLength(50)]
        public string? TimeZone { get; set; }

        public bool IsAllDay { get; set; } = false;

        [MaxLength(200)]
        public string? VenueName { get; set; }

        [MaxLength(500)]
        public string? Address { get; set; }

        [MaxLength(100)]
        public string? City { get; set; }

        [MaxLength(100)]
        public string? State { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        [MaxLength(20)]
        public string? PostalCode { get; set; }

        public bool IsVirtual { get; set; } = false;

        [MaxLength(500)]
        [Url]
        public string? VirtualMeetingUrl { get; set; }

        public int? MaxAttendess { get; set; }
        public bool IsPrivate { get; set; } = false;
        public bool RequiresApproval { get; set; } = false;
        public bool AllowGuestList { get; set; } = true;
        public int? ThemeId { get; set; }
        public string? CustomCss { get; set; }

        [MaxLength(500)]
        public string? BannerImageUrl { get; set; }

        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}
