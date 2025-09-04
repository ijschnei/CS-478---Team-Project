using System.ComponentModel.DataAnnotations;
namespace CS478_EventPlannerProject.DTOs.Message
{
    public class SendMessageDto
    {
        [Required]
        public string ReceiverId { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Subject { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [MaxLength(20)]
        public string MessageType { get; set; } = "direct";

        public int? RelatedEventId { get; set; }
    }
}
