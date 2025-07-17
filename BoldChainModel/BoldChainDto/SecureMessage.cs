namespace BoldChainBackendAPI.BoldChainModel.BoldChainDto
{
    public class SecureEmailMessage
    {
        public string To { get; set; } = string.Empty;
        public string From { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string HtmlBody { get; set; } = string.Empty;
        public string TextBody { get; set; } = string.Empty;
        public Dictionary<string, string> Headers { get; set; } = new();
        public List<EmailAttachment>? Attachments { get; set; } = new();
    }

}
