using ArtmaisBackend.Core.Profile.Dto;
using System.Collections.Generic;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IRecomendationMediator
    {
        public IEnumerable<RecomendationDto> Index(ClaimsPrincipal userClaims);
    }
}
