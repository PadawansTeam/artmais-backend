using ArtmaisBackend.Core.Entities;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IProfileAccessMediator
    {
        ProfileAcess? Create(ClaimsPrincipal visitorUserClaims, long visitedUserId);
    }
}
