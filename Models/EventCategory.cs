using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.Models
{
    public class EventCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(500)]
        public string? Description { get; set; }
        [MaxLength(300)]
        public string? IconUrl { get; set; }
        [MaxLength(7)]
        public string? ColorCode { get; set; } //hex color
        public bool IsActive { get; set; } = true;

        //Navigation Properties
        public virtual ICollection<EventCategoryMapping> EventMappings { get; set; } = new List<EventCategoryMapping>();

    }
}
