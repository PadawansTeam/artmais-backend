using ArtmaisBackend.Core.Entities;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IProfileAcessMediator
    {
        ProfileAcess Create(ClaimsPrincipal visitorUserClaims, long visitedUserId)
    }
}
