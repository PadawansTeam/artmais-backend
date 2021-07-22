using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Request;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        ShareLinkDto GetShareLink(UserRequest usernameRequest, string userName);

        ShareProfileBaseDto GetShareProfile(UserRequest usernameRequest);

        UserDto GetUserInfoById(int id);
    }
}
