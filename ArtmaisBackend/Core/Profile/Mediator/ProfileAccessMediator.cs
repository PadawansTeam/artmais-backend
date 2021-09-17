using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.SignIn.Service;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Mediator
{
    public class ProfileAccessMediator : IProfileAccessMediator
    {
        public ProfileAccessMediator(IProfileAccessRepository profileAcessRepository)
        {
            _profileAcessRepository = profileAcessRepository;
        }

        private readonly IProfileAccessRepository _profileAcessRepository;

        public ProfileAccess? Create(ClaimsPrincipal visitorUserClaims, long visitedUserId)
        {
            var visitorUserData = JwtTokenService.ReadToken(visitorUserClaims);

            if (visitorUserData.UserID.Equals(visitedUserId))
                return null;

            return _profileAcessRepository.Create(visitorUserData.UserID, visitedUserId);
        }
    }
}
