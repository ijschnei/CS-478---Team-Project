using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.DTOs.Attendee
{
    public class UpdateRSVPDto
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = string.Empty; // pending, accepted, declined, tentative

        [MaxLength(500)]
        public string? Notes { get; set; }
    }
}
