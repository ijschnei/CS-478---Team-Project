using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS478_EventPlannerProject.Models
{
    public class EventCustomFields
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FieldName { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string FieldType { get; set; } = string.Empty; //text, number, date, boolean, select, textarea
        public string? FieldValue { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public bool IsRequired { get; set; } = false;

        //for select fields - JSON array of options
        public string? SelectOptions { get; set; }

        //Navigation properties
        public virtual Events Event { get; set; } = null!;
    }   
}
