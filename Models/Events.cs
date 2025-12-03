using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace CS478_EventPlannerProject.Models
{
    public class Events
    {
        [Key]
        public int EventId { get; set; }

        [Required, MaxLength(200)]
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

        // Location info
        [MaxLength(200)]
        public string? VenueName { get; set; }

        public int? VenueId { get; set; }
        public Venue? Venue { get; set; }

        public int? VenueTimeSlotId { get; set; }
        public VenueTimeSlot? VenueTimeSlot { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }

        public bool IsVirtual { get; set; } = false;

        [MaxLength(500), Url]
        public string? VirtualMeetingUrl { get; set; }

        // event settings
        [Required]
        [ForeignKey("Creator")]
        public string CreatorId { get; set; } = string.Empty;

        public int? MaxAttendees { get; set; }
        public bool IsPrivate { get; set; } = false;
        public bool RequiresApproval { get; set; } = false;
        public bool AllowGuestList { get; set; } = true;

        // customization
        public int? ThemeId { get; set; }
        public string? CustomCss { get; set; }

        public string? BannerImageUrl { get; set; }

        [NotMapped]
        public IFormFile? BannerImageFile { get; set; }

        // metadata
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        // Navigation
        public Users Creator { get; set; } = null!;
        public EventTheme? Theme { get; set; }
        public ICollection<EventAttendees> Attendees { get; set; } = new List<EventAttendees>();
        public ICollection<EventCustomFields> CustomFields { get; set; } = new List<EventCustomFields>();
        public ICollection<EventCategoryMapping> Categories { get; set; } = new List<EventCategoryMapping>();
        public ICollection<Messages> RelatedMessages { get; set; } = new List<Messages>();

        // Computed
        [NotMapped] public int AttendeeCount => Attendees?.Count(a => a.Status == "accepted") ?? 0;
        [NotMapped] public bool IsUpcoming => StartDateTime > DateTime.UtcNow;
        [NotMapped] public bool IsOngoing => StartDateTime <= DateTime.UtcNow && (EndDateTime == null || EndDateTime > DateTime.UtcNow);
        [NotMapped] public bool IsPast => EndDateTime?.CompareTo(DateTime.UtcNow) < 0 || (EndDateTime == null && StartDateTime < DateTime.UtcNow);
        [NotMapped]
        public string FormattedLocation => IsVirtual ? "Virtual Event"
            : !string.IsNullOrEmpty(VenueName) ? $"{VenueName}, {City}"
            : !string.IsNullOrEmpty(City) ? $"{City}, {State}"
            : "Location TBD";
    }
}
