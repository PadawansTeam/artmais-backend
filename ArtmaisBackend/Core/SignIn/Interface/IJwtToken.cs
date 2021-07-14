using ArtmaisBackend.Core.Entities;
using System.Security.Claims;

namespace ArtmaisBackend.Core.SignIn.Interface
{
    public interface IJwtToken
    {
        public string GenerateToken(User usuario);
        public UserJwtData ReadToken(ClaimsPrincipal userClaims);
    }
}
