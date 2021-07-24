using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util;

namespace ArtmaisBackend.Core.SignIn.Service
{
    public class SignInService : ISignInService
    {
        public SignInService(IUserRepository userRepository, IJwtTokenService jwtToken)
        {
            this._userRepository = userRepository;
            this._jwtToken = jwtToken;
        }

        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtToken;

        public string Authenticate(SigInRequest sigInRequest)
        {
            var user = this._userRepository.GetUserByEmail(sigInRequest.Email);

            if (user == null)
                throw new Unauthorized("Usuário e/ou senha inválidos");

            var salt = user.Password.Substring(0, 24);
            var encryptedPassword = PasswordUtil.Encrypt(sigInRequest.Password, salt);

            if (encryptedPassword.Equals(user.Password))
                return this._jwtToken.GenerateToken(user);

            throw new Unauthorized("Usuário e/ou senha inválidos");
        }
    }
}
