using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Request;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        ShareLinkDto GetShareLink(UsernameRequest usernameRequest, string userName);

        SharePerfilBaseDto GetShareProfile(UsernameRequest usernameRequest);
    }
}
