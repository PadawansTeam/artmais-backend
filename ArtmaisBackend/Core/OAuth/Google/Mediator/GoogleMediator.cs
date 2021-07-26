using ArtmaisBackend.Core.OAuth.Google.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Services.Interface;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.OAuth.Google.Mediator
{
    public class GoogleMediator : IGoogleMediator
    {
        public GoogleMediator(IGoogleService googleService, IJwtTokenService jwtTokenService, IUserRepository userRepository)
        {
            _googleService = googleService;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        private readonly IGoogleService _googleService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        public async Task<string?> SignIn(string token)
        {
            var googleUser = await _googleService.ValidateToken(token);
            var user = _userRepository.GetUserById(long.Parse(googleUser.Subject));

            if (user == null)
                return null;

            return _jwtTokenService.GenerateToken(user);
        }

        public string SignUp(string token)
        {
            
        }
    }
}
