using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.Models
{
    public class Users : IdentityUser
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLoginAt { get; set; }
        [Required]
        public bool IsActive {  get; set; }=true;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        //Navigation Properties
        public virtual UserProfiles? Profile { get; set; }
        public virtual ICollection<Events> CreatedEvents { get; set; } = new List<Events>();
        public virtual ICollection<EventAttendees> EventAttendances { get; set; } = new List<EventAttendees>();
        public virtual ICollection<Messages> SentMessages { get; set; } = new List<Messages>();
        public virtual ICollection<Messages> ReceivedMessages { get; set; } = new List<Messages>();
    }
}
