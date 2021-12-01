using ArtmaisBackend.Core.Mail.Requests;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Mail.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
}
