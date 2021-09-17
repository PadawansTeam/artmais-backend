using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.SignIn.Service;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Collections.Generic;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Mediator
{
    public class RecomendationMediator : IRecomendationMediator
    {
        public RecomendationMediator(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;

        public IEnumerable<RecomendationDto> Index(ClaimsPrincipal userClaims)
        {
            var userJwtData = JwtTokenService.ReadToken(userClaims);
            return this._userRepository.GetUsersByInterest(userJwtData.UserID);
        }

        public IEnumerable<RecomendationDto> GetUsers()
        {
            return this._userRepository.GetUsers();
        }
    }
}
