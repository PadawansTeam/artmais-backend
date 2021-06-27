using oauth_poc.Core.Entities;

namespace oauth_poc.Core.SignIn.Interface
{
    public interface IJwtToken
    {
        public dynamic GenerateToken(User usuario);
    }
}
