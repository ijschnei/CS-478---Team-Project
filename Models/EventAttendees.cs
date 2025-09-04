using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS478_EventPlannerProject.Models
{
    public class EventAttendees
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "pending"; //pending, accepted, declined, tentative
        [Required]
        [MaxLength(20)]
        public string AttendeeType { get; set; } = "attendee"; //organizer, co-organizer, attendee
        [Required]
        public DateTime RSVP_Date { get; set; } = DateTime.UtcNow;
        public bool CheckedIn { get; set; } = false;
        public DateTime? CheckInTime { get; set; }
        [MaxLength(500)]
        public string? Notes { get; set; }

        //navigation properties
        public virtual Events Event { get; set; } = null!;
        public virtual Users User { get; set; } = null!;

    }
}
