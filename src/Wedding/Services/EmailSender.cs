using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace Wedding.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _client;
        private readonly ILogger<EmailSender> _logger;
        private readonly string _emailAddressSender;

        public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
        {
            _logger = logger;

            var host = configuration.GetValue<string>("EmailHost");
            var port = configuration.GetValue<int>("EmailPort");
            var enableSsl = configuration.GetValue<bool>("EmailSslEnabled");
            var userName = configuration.GetValue<string>("EmailUserName");
            var password = configuration.GetValue<string>("EmailPassword");

            _emailAddressSender = configuration.GetValue<string>("EmailAddressSender");

            // Initialize the smtp client with the given parameters
            _client = new SmtpClient(host)
            {
                Port = port,
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(userName, password)
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            // Create a new mail message with the given parameters
            var message = new MailMessage
            {
                From = new MailAddress(_emailAddressSender),
                To = { new MailAddress(toEmail) },
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            // Send the message asynchronously using the smtp client
            await _client.SendMailAsync(message);
        }
    }
}
