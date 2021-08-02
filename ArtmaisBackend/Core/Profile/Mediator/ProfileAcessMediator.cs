using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Mediator
{
    public class ProfileAcessMediator : IProfileAcessMediator
    {
        public ProfileAcessMediator(IJwtTokenService jwtTokenService, IProfileAcessRepository profileAcessRepository)
        {
            _jwtTokenService = jwtTokenService;
            _profileAcessRepository = profileAcessRepository;
        }

        private readonly IJwtTokenService _jwtTokenService;
        private readonly IProfileAcessRepository _profileAcessRepository;

        public ProfileAcess Create(ClaimsPrincipal visitorUserClaims, long visitedUserId)
        {
            var visitorUserData = _jwtTokenService.ReadToken(visitorUserClaims);

            if (visitorUserData.UserID.Equals(visitedUserId))
                return null;

            return _profileAcessRepository.Create(visitorUserData.UserID, visitedUserId);
        }
    }
}
