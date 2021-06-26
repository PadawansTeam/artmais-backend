using oauth_poc.Core.Entities;
using oauth_poc.Core.SignUp;

namespace oauth_poc.Infrastructure.Repository.Interface
{
    public interface IUsuarioRepository
    {
        public Usuario Create(SignUpRequest signUpRequest);
        public Usuario GetUsuarioByEmail(string email);
    }
}
