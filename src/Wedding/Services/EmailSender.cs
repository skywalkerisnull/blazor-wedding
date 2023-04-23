using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using DocumentFormat.OpenXml.Spreadsheet;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Wedding.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridClient _client;
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

            _client = new SendGridClient(password);
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {

            var from = new EmailAddress(_emailAddressSender, "Wedding Site");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, body);
            var result = await _client.SendEmailAsync(msg);

            _logger.LogInformation(result.ToString());
        }
    }
}
