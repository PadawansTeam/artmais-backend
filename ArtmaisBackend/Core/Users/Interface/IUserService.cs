using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Request;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        ShareLinkDto? GetShareLinkByLoggedUser(long? userId);

        ShareLinkDto? GetShareLinkByUserId(long? userId);

        ShareProfileBaseDto? GetShareProfile(long? userId);

        UserDto? GetLoggedUserInfoById(long? id);

        UserDto? GetUserInfoById(long? id);

        UserProfileInfoDto UpdateUserInfo(UserRequest? userRequest, long userId);
        
        bool UpdateUserPassword(PasswordRequest? passwordRequest, long userId);

        bool UpdateUserDescription(DescriptionRequest? descriptionRequest, long userId);
    }
}
