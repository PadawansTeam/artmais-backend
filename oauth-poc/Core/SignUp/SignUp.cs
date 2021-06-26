using oauth_poc.Core.SignIn.Interface;
using oauth_poc.Core.SignUp.Interface;
using oauth_poc.Infrastructure.Repository.Interface;
using oauth_poc.Util;

namespace oauth_poc.Core.SignUp
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
            signUpRequest.Password = PasswordUtil.Encrypt(signUpRequest.Password);
            var user = _usuarioRepository.Create(signUpRequest);

            return _jwtToken.GenerateToken(user);
        }
    }
}
