namespace BoldChainBackendAPI.BoldChainService
{
    using BoldChainBackendAPI.BoldChainConfiguration;
    using BoldChainBackendAPI.BoldChainInterface;
    using BoldChainBackendAPI.BoldChainModel.BoldChainDto;
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Options;
    using MimeKit;

    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(SecureEmailMessage message)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(message.From ?? _settings.FromAddress));
            email.To.Add(MailboxAddress.Parse(message.To));
            email.Subject = message.Subject;

            // Add headers like X-BoldChain-Signature, X-BoldChain-Sender-Wallet, etc.
            foreach (var header in message.Headers)
            {
                email.Headers.Add(header.Key, header.Value);
            }

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = message.HtmlBody,
                TextBody = message.TextBody
            };

            if (message.Attachments != null)
            {
                foreach (var attachment in message.Attachments)
                {
                    bodyBuilder.Attachments.Add(attachment.FileName, attachment.Content, ContentType.Parse(attachment.ContentType));
                }
            }

            email.Body = bodyBuilder.ToMessageBody();

            if (_settings.SendRealEmails)
            {
                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_settings.SmtpHost, _settings.SmtpPort, _settings.EnableSsl);
                await smtp.AuthenticateAsync(_settings.SmtpUser, _settings.SmtpPass);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }
            else
            {
                Console.WriteLine("🔒 Email sending is disabled (SendRealEmails = false)");
                Console.WriteLine($"To: {message.To}");
                Console.WriteLine($"Subject: {message.Subject}");
                Console.WriteLine($"Body: {message.TextBody}");
            }
        }
    }

}
