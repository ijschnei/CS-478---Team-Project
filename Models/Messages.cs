using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS478_EventPlannerProject.Models
{
    public class Messages
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Sender")]
        public string SenderId { get; set; } = string.Empty;
        [Required]
        [ForeignKey("Receiver")]
        public string ReceiverId { get; set; } = string.Empty;
        [Required]
        public Guid ConversationId { get; set; }
        [MaxLength(200)]
        public string? Subject { get; set; }
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string MessageType { get; set; } = "direct"; //direct, event_related, system
        [ForeignKey("RelatedEvent")]
        public int? RelatedEventId { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        [Required]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public bool DeletedBySender { get; set; } = false;
        public bool DeletedByReceiver { get; set; } = false;

        //Navigation Properties
        public virtual Users Sender { get; set; } = null!;
        public virtual Users Receiver { get; set; } = null!;
        public virtual Events? RelatedEvent { get; set; }

    }
}
