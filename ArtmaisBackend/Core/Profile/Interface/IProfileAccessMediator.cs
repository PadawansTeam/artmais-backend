using ArtmaisBackend.Core.Entities;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IProfileAccessMediator
    {
        ProfileAccess? Create(ClaimsPrincipal visitorUserClaims, long visitedUserId);
    }
}
