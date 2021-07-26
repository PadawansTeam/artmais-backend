using ArtmaisBackend.Core.OAuth.Google.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Services.Interface;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.OAuth.Google.Mediator
{
    public class GoogleMediator : IGoogleMediator
    {
        public GoogleMediator(ICategorySubcategoryRepository categorySubcategoryRepository, IExternalAuthorizationRepository externalAuthorizationRepository, IGoogleService googleService, IJwtTokenService jwtTokenService, IUserRepository userRepository)
        {
            _categorySubcategoryRepository = categorySubcategoryRepository;
            _externalAuthorizationRepository = externalAuthorizationRepository;
            _googleService = googleService;
            _jwtTokenService = jwtTokenService;
            _userRepository = userRepository;
        }

        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IExternalAuthorizationRepository _externalAuthorizationRepository;
        private readonly IGoogleService _googleService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IUserRepository _userRepository;

        public async Task<string?> SignIn(string token)
        {
            var googleUser = await _googleService.ValidateToken(token);
            var externalAuthorization = _externalAuthorizationRepository.GetExternalAuthorizationByExternalAuthorizationId(googleUser.Subject);

            if (externalAuthorization == null)
                throw new UserNotFound("Usuário não encontrado");

            var user = _userRepository.GetUserById(externalAuthorization.UserId);

            if (user == null)
                return null;

            return _jwtTokenService.GenerateToken(user);
        }

        public string SignUp(OAuthSignUpRequest request)
        {
            var existentUsername = this._userRepository.GetUserByUsername(request.Username);

            if (existentUsername != null)
                throw new UsernameAlreadyInUse("Username já utilizado.");

            var existentSubcategory = this._categorySubcategoryRepository
                .GetSubcategoryBySubcategory(request.Subcategory);

            if (existentSubcategory == null)
                existentSubcategory = this._categorySubcategoryRepository.Create(request.Category, request.Subcategory);

            request.SubcategoryID = existentSubcategory.SubcategoryID;

            var user = _userRepository.CreateOAuthUser(request, "google");
            _externalAuthorizationRepository.Create(request.ExternalAuthorizationId, user.UserID);

            return _jwtTokenService.GenerateToken(user);
        }
    }
}