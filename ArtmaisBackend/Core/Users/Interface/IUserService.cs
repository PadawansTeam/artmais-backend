using ArtmaisBackend.Core.Users.Dto;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        ShareLinkDto? GetShareLinkByLoggedUser(int? userId);

        ShareLinkDto? GetShareLinkByUserId(int? userId);

        ShareProfileBaseDto? GetShareProfile(int? userId);

        UserDto? GetLoggedUserInfoById(int? id);

        UserDto? GetUserInfoById(int? id);
    }
}
