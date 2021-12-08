using ArtmaisBackend.Core.PasswordRecovery.Dtos;
using ArtmaisBackend.Core.PasswordRecovery.Requests;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.PasswordRecovery.Services
{
    public interface IPasswordRecoveryService
    {
        Task<PasswordRecoveryDto> CreateAsync(string email);
        Task<Entities.PasswordRecovery> ValidateCodeAsync(long userId, int code);
        void UpdatePassword(PasswordRecoveryRequest passwordRecoveryRequest);
    }
}
