using artmais_backend.Core.SignIn.Interface;
using artmais_backend.Exceptions;
using artmais_backend.Infrastructure.Repository.Interface;
using artmais_backend.Util;

namespace artmais_backend.Core.SignIn
{
    public class SignIn : ISignIn
    {
        public SignIn(IUserRepository usuarioRepository, IJwtToken jwtToken)
        {
            _usuarioRepository = usuarioRepository;
            _jwtToken = jwtToken;
        }

        private readonly IUserRepository _usuarioRepository;
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
