namespace hotelier_core_app.Model.DTOs.Request
{
    public class SendEmailDTO
    {
        public List<string>? Recipient { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? TemplateString { get; set; }
        public Dictionary<string, Stream>? Attachment { get; set; }
        public List<string>? Cc { get; set; }

        public SendEmailDTO(List<string> email, string subject, string message, string? templateString = null)
        {
            Recipient = email;
            Subject = subject;
            Message = message;
            Name = string.Empty;
            TemplateString = templateString;
        }
    }
}
