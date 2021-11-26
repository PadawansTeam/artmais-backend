using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Responses;
using System.Collections.Generic;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IRecomendationMediator
    {
        RecommendationResponse Index(ClaimsPrincipal userClaims);
        IEnumerable<RecommendationDto> GetUsers();
    }
}
