using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Request;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        ShareLinkDto? GetShareLinkByLoggedUser(int? userId);

        ShareLinkDto? GetShareLinkByUserId(int? userId);

        ShareProfileBaseDto? GetShareProfile(int? userId);

        UserDto? GetLoggedUserInfoById(int? id);

        UserDto? GetUserInfoById(int? id);

        UserProfileInfoDto UpdateUserInfo(UserRequest? userRequest, int userId);
        
        bool UpdateUserPassword(PasswordRequest? passwordRequest, int userId);

        bool UpdateUserDescription(DescriptionRequest? descriptionRequest, int userId);
    }
}
