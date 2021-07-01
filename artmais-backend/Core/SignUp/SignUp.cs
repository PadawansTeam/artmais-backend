using artmais_backend.Core.SignIn.Interface;
using artmais_backend.Core.SignUp.Interface;
using artmais_backend.Exceptions;
using artmais_backend.Infrastructure.Repository.Interface;
using artmais_backend.Util;

namespace artmais_backend.Core.SignUp
{
    public class SignUp : ISignUp
    {
        public SignUp(IUserRepository usuarioRepository, IJwtToken jwtToken)
        {
            _usuarioRepository = usuarioRepository;
            _jwtToken = jwtToken;
        }

        private readonly IUserRepository _usuarioRepository;
        private readonly IJwtToken _jwtToken;

        public string Create(SignUpRequest signUpRequest)
        {
            var existentUser = _usuarioRepository.GetUsuarioByEmail(signUpRequest.Email);

            if (existentUser != null)
                throw new EmailAlreadyInUse("E-mail já utilizado");

            signUpRequest.Password = PasswordUtil.Encrypt(signUpRequest.Password);
            var user = _usuarioRepository.Create(signUpRequest);

            return _jwtToken.GenerateToken(user);
        }
    }
}
