using artmais_backend.Core.Entities;
using artmais_backend.Core.SignUp;

namespace artmais_backend.Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        public User Create(SignUpRequest signUpRequest);
        public User GetUsuarioByEmail(string email);
    }
}
