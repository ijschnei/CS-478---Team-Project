using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.Models
{
    public class EventTheme
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Description { get; set; }    
        public string? CssTemplate { get; set; }
        [MaxLength(300)]
        public string? ThumbnailUrl { get; set; }
        public bool IsPremium { get; set; } = false;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        //Navigation Properties
        public virtual ICollection<Events> Events { get; set; } = new List<Events>();
    }
}
