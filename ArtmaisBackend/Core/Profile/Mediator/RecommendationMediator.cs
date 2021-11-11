using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.Profile.Responses;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Collections.Generic;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Mediator
{
    public class RecommendationMediator : IRecomendationMediator
    {
        public RecommendationMediator(IUserRepository userRepository, IJwtTokenService jwtToken)
        {
            _userRepository = userRepository;
            this._jwtToken = jwtToken;
        }

        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtToken;

        public RecommendationResponse Index(ClaimsPrincipal userClaims)
        {
            var response = new RecommendationResponse();

            var userJwtData = this._jwtToken.ReadToken(userClaims);

            response.UserInterests = _userRepository.GetUsersByInterest(userJwtData.UserID);
            response.RecommendedInterests = _userRepository.GetUsersByRecommendatin(userJwtData.UserID);

            return response;
        }

        public IEnumerable<RecommendationDto> GetUsers()
        {
            return this._userRepository.GetUsers();
        }
    }
}
