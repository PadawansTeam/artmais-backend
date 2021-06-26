using oauth_poc.Core.Entities;
using oauth_poc.Core.SignUp;

namespace oauth_poc.Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        public User Create(SignUpRequest signUpRequest);
        public User GetUsuarioByEmail(string email);
    }
}
