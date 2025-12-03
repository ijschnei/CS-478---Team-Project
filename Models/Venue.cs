using System;
using System.Collections.Generic;

namespace CS478_EventPlannerProject.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string? VenueType { get; set; }
        public int Capacity { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Amenities { get; set; }
        public string? Description { get; set; }

        // Navigation
        public ICollection<Events> Events { get; set; } = new List<Events>();
        public ICollection<VenueTimeSlot> TimeSlots { get; set; } = new List<VenueTimeSlot>();

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
