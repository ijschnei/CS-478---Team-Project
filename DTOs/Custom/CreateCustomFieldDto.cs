using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.DTOs.Custom
{
    public class CreateCustomFieldDto
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FieldName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string FieldType { get; set; } = string.Empty;

        public string? FieldValue { get; set; }
        public int DisplayOrder { get; set; } = 0;
        public bool IsRequired { get; set; } = false;
        public List<string>? SelectOptions { get; set; }
    }
}
