using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS478_EventPlannerProject.Models
{
    public class Events
    {
        [Key]
        public int EventId { get; set; }
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
        // Location details
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
        //[Column(TypeName = "decimal(10,8)")]
        //public decimal? Latitude { get; set; }

        //[Column(TypeName = "decimal(11,8)")]
        //public decimal? Longitude { get; set; }

        public bool IsVirtual { get; set; } = false;

        [MaxLength(500)]
        [Url]
        public string? VirtualMeetingUrl { get; set; }

        //event settings
        [Required]
        [ForeignKey("Creator")]
        public string CreatorId { get; set; } = string.Empty;
        public int? MaxAttendees { get; set; }
        public bool IsPrivate { get; set; } = false;
        public bool RequiresApproval { get; set; } = false;
        public bool AllowGuestList { get; set; } = true;

        //event customization
        public int? ThemeId { get; set; }
        public string? CustomCss { get; set; }
        [MaxLength(500)]
        public string? BannerImageUrl { get; set; }

        [NotMapped]
        public IFormFile? BannerImageFile { get; set; }
        //Metadata
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        //Navigation Properties
        public virtual Users Creator { get; set; } = null!;
        public virtual EventTheme? Theme { get; set; }
        public virtual ICollection<EventAttendees> Attendees { get; set; } = new List<EventAttendees>();
        public virtual ICollection<EventCustomFields> CustomFields { get; set; } = new List<EventCustomFields>();
        public virtual ICollection<EventCategoryMapping> Categories { get; set; } = new List<EventCategoryMapping>();
        public virtual ICollection<Messages> RelatedMessages { get; set; } = new List<Messages>();

        //computed properties
        [NotMapped]
        public int AttendeeCount => Attendees?.Count(a => a.Status == "accepted") ?? 0;
        [NotMapped]
        public bool IsUpcoming => StartDateTime > DateTime.UtcNow;
        [NotMapped]
        public bool IsOngoing => StartDateTime <= DateTime.UtcNow &&
            (EndDateTime == null || EndDateTime > DateTime.UtcNow);
        [NotMapped]
        public bool IsPast => EndDateTime?.CompareTo(DateTime.UtcNow) < 0 ||
            (EndDateTime == null && StartDateTime < DateTime.UtcNow);
        [NotMapped]
        public string FormattedLocation => IsVirtual ? "Virtual Event" :
            !string.IsNullOrEmpty(VenueName) ? $"{VenueName}, {City}" :
            !string.IsNullOrEmpty(City) ? $"{City}, {State}" : "Location TBD";
    }
}