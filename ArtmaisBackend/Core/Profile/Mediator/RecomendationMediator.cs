using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Collections.Generic;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Mediator
{
    public class RecomendationMediator : IRecomendationMediator
    {
        public RecomendationMediator(IUserRepository userRepository, IJwtToken jwtToken)
        {
            _userRepository = userRepository;
            _jwtToken = jwtToken;
        }

        private readonly IUserRepository _userRepository;
        private readonly IJwtToken _jwtToken;

        public IEnumerable<RecomendationDto> Index(ClaimsPrincipal userClaims)
        {
            var userJwtData = _jwtToken.ReadToken(userClaims);
            return _userRepository.GetUsersByInterest(userJwtData.UserID);
        }
    }
}
