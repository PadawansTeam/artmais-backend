using ArtmaisBackend.Core.Users.Dto;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        Task<ShareLinkDto> GetShareLinkAsync(int userId, string userName);
    }
}
