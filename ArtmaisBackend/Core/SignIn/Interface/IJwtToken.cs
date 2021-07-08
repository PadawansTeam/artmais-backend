using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Core.SignIn.Interface
{
    public interface IJwtToken
    {
        public string GenerateToken(User usuario);
    }
}
