using ArtmaisBackend.Core.Profile.Dto;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IInterestMediator
    {
        public InterestDto Index(ClaimsPrincipal userClaims);
        public dynamic Create(InterestRequest interestRequest, ClaimsPrincipal userClaims);
    }
}
