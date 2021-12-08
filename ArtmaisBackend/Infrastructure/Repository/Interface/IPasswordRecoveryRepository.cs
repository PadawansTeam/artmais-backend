using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPasswordRecoveryRepository
    {
        Task<PasswordRecovery> CreateAsync(long userId, int code);
        Task<PasswordRecovery?> GetByUserIdAndCodeAsync(long userId, int code);
    }
}
