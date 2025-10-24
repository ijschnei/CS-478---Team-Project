using System.ComponentModel.DataAnnotations;
using CS478_EventPlannerProject.Models;
//comment for testing github commit, ignore
namespace CS478_EventPlannerProject.ViewModels
{
    public class ConversationViewModel
    {
        public Guid ConversationId { get; set; }
        public Messages LastMessage { get; set; } = null!;
        public int UnreadCount { get; set; }
        public int MessageCount { get; set; }
    }

    public class ComposeMessageViewModel
    {
        [Required(ErrorMessage ="Recipient is required")]
        public string ReceiverId { get; set; } = string.Empty;
        public string? ReceiverName { get; set; }
        [MaxLength(200)]
        public string? Subject { get; set; }
        [Required(ErrorMessage ="Message content is required")]
        [MinLength(1, ErrorMessage = "Message cannot be empty")]
        public string Content { get; set; } = string.Empty;
        public string? MessageType { get; set; } = "direct";
        public int? RelatedEventId { get; set; }
        public string? EventName { get; set; }
    }

    public class QuickReplyModel
    {
        [Required]
        public Guid ConversationId { get; set; }
        [Required(ErrorMessage ="Message content is required")]
        public string Content { get; set; } = string.Empty;
        public string? MessageType { get; set; }
        public int? RelatedEventId { get; set; }
    }
}
