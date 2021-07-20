using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util;

namespace ArtmaisBackend.Core.SignIn.Service
{
    public class SignInService : ISignIn
    {
        public SignInService(IUserRepository userRepository, IJwtToken jwtToken)
        {
            _userRepository = userRepository;
            _jwtToken = jwtToken;
        }

        private readonly IUserRepository _userRepository;
        private readonly IJwtToken _jwtToken;

        public string Authenticate(SigInRequest sigInRequest)
        {
            var user = _userRepository.GetUserByEmail(sigInRequest.Email);

            if (user == null)
                throw new Unauthorized("Usuário e/ou senha inválidos");

            var salt = user.Password.Substring(0, 24);
            var encryptedPassword = PasswordUtil.Encrypt(sigInRequest.Password, salt);

            if (encryptedPassword.Equals(user.Password))
                return _jwtToken.GenerateToken(user);

            throw new Unauthorized("Usuário e/ou senha inválidos");
        }
    }
}
