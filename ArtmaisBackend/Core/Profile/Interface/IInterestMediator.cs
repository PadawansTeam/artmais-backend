using ArtmaisBackend.Core.Profile.Dto;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IInterestMediator
    {
        InterestDto Index(ClaimsPrincipal userClaims);
        Task<MessageDto> Create(InterestRequest interestRequest, ClaimsPrincipal userClaims);
    }
}
