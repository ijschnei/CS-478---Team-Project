namespace CS478_EventPlannerProject.DTOs.Message
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string ReceiverId { get; set; } = string.Empty;
        public string ReceiverName { get; set; } = string.Empty;
        public Guid ConversationId { get; set; }
        public string? Subject { get; set; }
        public string Content { get; set; } = string.Empty;
        public string MessageType { get; set; } = string.Empty;
        public int? RelatedEventId { get; set; }
        public string? RelatedEventName { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }
        public DateTime SentAt { get; set; }
    }
}
