using artmais_backend.Core.Entities;

namespace artmais_backend.Core.SignIn.Interface
{
    public interface IJwtToken
    {
        public string GenerateToken(User usuario);
    }
}
