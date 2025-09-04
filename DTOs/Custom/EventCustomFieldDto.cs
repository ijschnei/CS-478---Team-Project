namespace CS478_EventPlannerProject.DTOs.Custom
{
    public class EventCustomFieldDto
    {
        public int Id { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string FieldType { get; set; } = string.Empty;
        public string? FieldValue { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRequired { get; set; }
        public List<string>? SelectOptions { get; set; }
    }
}
