using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util;

namespace ArtmaisBackend.Core.SignIn.Service
{
    public class SignInService : ISignInService
    {
        public SignInService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;

        public string Authenticate(SigInRequest signInRequest)
        {
            var user = this._userRepository.GetUserByEmail(signInRequest.Email);

            if (user == null)
                throw new Unauthorized("Usuário e/ou senha inválidos");

            if (signInRequest.Password.Equals("") || signInRequest.Password == null)
                throw new Unauthorized("Usuário e/ou senha inválidos");

            var salt = user.Password.Substring(0, 24);
            var encryptedPassword = PasswordUtil.Encrypt(signInRequest.Password, salt);

            if (encryptedPassword.Equals(user.Password))
                return JwtTokenService.GenerateToken(user);

            throw new Unauthorized("Usuário e/ou senha inválidos");
        }
    }
}
