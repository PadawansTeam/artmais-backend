using ArtmaisBackend.Core.Users.Dto;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        ShareLinkDto? GetShareLink(int? userId, int userIdProfile);

        ShareProfileBaseDto? GetShareProfile(int? userId);

        UserDto? GetUserInfoById(int? id);
    }
}
