using ArtmaisBackend.Core.Profile.Dto;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IInterestMediator
    {
        InterestDto Index(ClaimsPrincipal userClaims);
        dynamic Create(InterestRequest interestRequest, ClaimsPrincipal userClaims);
    }
}
