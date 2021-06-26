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
            var usuario = _usuarioRepository.GetUsuarioByEmail(sigInRequest.Email);

            if (usuario == null)
                throw new Unauthorized("Usuário e/ou senha inválidos");

            var salt = usuario.Senha.Split("/")[0];
            var encryptedPassword = PasswordUtil.Encrypt(sigInRequest.Senha, salt);

            if (encryptedPassword.Equals(usuario.Senha))
                return _jwtToken.GenerateToken(usuario);

            throw new Unauthorized("Usuário e/ou senha inválidos");
        }
    }
}
