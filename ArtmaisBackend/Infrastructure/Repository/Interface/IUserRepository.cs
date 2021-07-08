using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.SignUp;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        public User Create(SignUpRequest signUpRequest);
        public User GetUsuarioByEmail(string email);
    }
}
