using ArtmaisBackend.Core.Entities;
using System.Security.Claims;

namespace ArtmaisBackend.Core.SignIn.Interface
{
    public interface IJwtTokenService
    {
        string GenerateToken(User usuario);
        UserJwtData ReadToken(ClaimsPrincipal userClaims);
    }
}