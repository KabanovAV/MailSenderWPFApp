namespace MailSender.Entities
{
    public class MailData
    {
        public string DisplayName { get; set; } = default!;
        public string To { get; set; } = default!;
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public AttachmentData? Attachment { get; set; }
    }
}
