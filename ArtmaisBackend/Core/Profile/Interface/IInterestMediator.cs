using ArtmaisBackend.Core.Entities;
using System.Collections.Generic;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IInterestMediator
    {
        public IEnumerable<SubcategoryDto> Index();
        public IEnumerable<Interest> Create(InterestRequest interestRequest, ClaimsPrincipal userClaims);
    }
}
