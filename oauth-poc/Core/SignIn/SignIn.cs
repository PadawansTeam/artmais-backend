using oauth_poc.Core.SignIn.Interface;
using oauth_poc.Exceptions;
using oauth_poc.Infrastructure.Repository.Interface;
using oauth_poc.Util;

namespace oauth_poc.Core.SignIn
{
    public class SignIn : ISignIn
    {
        public SignIn(IUsuarioRepository usuarioRepository, IJwtToken jwtToken)
        {
            _usuarioRepository = usuarioRepository;
            _jwtToken = jwtToken;
        }

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IJwtToken _jwtToken;

        public string Authenticate(SigInRequest sigInRequest)
        {
            var user = _usuarioRepository.GetUsuarioByEmail(sigInRequest.Email);

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
