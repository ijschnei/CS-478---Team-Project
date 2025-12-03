using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS478_EventPlannerProject.Models
{
    public class VenueTimeSlot
    {
        [Key]
        public int TimeSlotId { get; set; }

        [Required]
        [ForeignKey("Venue")]
        public int VenueId { get; set; }

        [Required]
        public DateTime SlotDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int? BookedEventId { get; set; }
        public virtual Events? BookedEvent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual Venue Venue { get; set; } = null!;

        // Computed
        [NotMapped]
        public DateTime StartDateTime => SlotDate.Date + StartTime;

        [NotMapped]
        public DateTime EndDateTime => SlotDate.Date + EndTime;

        [NotMapped]
        public string FormattedTimeRange => $"{StartTime:hh\\:mm} - {EndTime:hh\\:mm}";

        [NotMapped]
        public string FormattedDateTime => $"{SlotDate:MMM dd, yyyy} {FormattedTimeRange}";
    }
}
