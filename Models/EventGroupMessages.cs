using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CS478_EventPlannerProject.Models
{
    public class EventGroupMessages
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Event")]
        public int EventId { get; set; }
        [Required]
        [ForeignKey("Sender")]
        public string SenderId { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        [MaxLength(30)]
        public string MessageType { get; set; } = "chat"; //chat, announcement, alert, system
        [MaxLength(200)]
        public string? Subject { get; set; }
        [Required]
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
        public bool IsPinned { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        //Navigation Properties
        public virtual Events Event { get; set; } = null!;
        public virtual Users Sender { get; set; } = null!;
        public virtual ICollection<EventGroupMessageReads> MessageReads { get; set; } = new List<EventGroupMessageReads>();
    }
    //track who had read group messages
    public class EventGroupMessageReads
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Message")]
        public int MessageId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public DateTime ReadAt { get; set; } = DateTime.UtcNow;
        //navigation properties
        public virtual EventGroupMessages Message { get; set; } = null!;
        public virtual Users User { get; set; } = null!;
    }
}
