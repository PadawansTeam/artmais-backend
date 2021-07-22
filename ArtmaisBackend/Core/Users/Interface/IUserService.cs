using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Request;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        ShareLinkDto GetShareLink(UserRequest userId, int userIdProfile);

        ShareProfileBaseDto GetShareProfile(UserRequest userId);

        UserDto GetUserInfoById(int? id);
    }
}
