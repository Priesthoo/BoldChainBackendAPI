namespace BoldChainBackendAPI.BoldChainConfiguration
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; }
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPass { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
        public bool SendRealEmails { get; set; } = true; // test/dev switch
    }

}
