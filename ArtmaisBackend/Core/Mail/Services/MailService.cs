using ArtmaisBackend.Core.Mail.Requests;
using ArtmaisBackend.Infrastructure.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Mail.Services
{
    [ExcludeFromCodeCoverage]
    public class MailService : IMailService
    {
        private readonly EmailConfiguration _emailConfiguration;
        private readonly IConfiguration _configuration;

        public MailService(IOptions<EmailConfiguration> emailConfiguration, IConfiguration configuration)
        {
            _emailConfiguration = emailConfiguration.Value;
            _configuration = configuration;
        }

        public async Task SendEmailAsync(EmailRequest emailRequest)
        {
            var message = new MailMessage();
            var smtp = new SmtpClient();

            message.From = new MailAddress(_emailConfiguration.Mail, _emailConfiguration.DisplayName);
            message.To.Add(new MailAddress(emailRequest.ToEmail));
            message.Subject = emailRequest.Subject;

            message.IsBodyHtml = true;
            message.Body = emailRequest.Body;
            smtp.Port = _emailConfiguration.Port;
            smtp.Host = _emailConfiguration.Host;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(_emailConfiguration.Mail, _configuration["EMAIL_PASSWORD"]);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

            await smtp.SendMailAsync(message);
        }
    }
}
