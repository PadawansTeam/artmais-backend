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
        public RecomendationMediator(IUserRepository userRepository, IJwtTokenService jwtToken)
        {
            this._userRepository = userRepository;
            this._jwtToken = jwtToken;
        }

        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtToken;

        public IEnumerable<RecomendationDto> Index(ClaimsPrincipal userClaims)
        {
            var userJwtData = this._jwtToken.ReadToken(userClaims);
            return this._userRepository.GetUsersByInterest(userJwtData.UserID);
        }
    }
}
