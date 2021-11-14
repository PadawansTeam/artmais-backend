using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Request;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Users.Interface
{
    public interface IUserService
    {
        ShareLinkDto? GetShareLinkByLoggedUser(long? userId);

        ShareLinkDto? GetShareLinkByUserId(long? userId);

        ShareProfileBaseDto? GetShareProfile(long? userId);

        Task<UserDto?> GetLoggedUserInfoById(long? userId);

        Task<UserDto?> GetUserInfoById(long? userId);

        UserProfileInfoDto UpdateUserInfo(UserRequest? userRequest, long userId);
        
        bool UpdateUserPassword(PasswordRequest? passwordRequest, long userId);

        bool UpdateUserDescription(UserDescriptionRequest? descriptionRequest, long userId);
    }
}
