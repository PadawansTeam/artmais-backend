using ArtmaisBackend.Core.Profile.Dto;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.Profile.Responses
{
    public class RecommendationResponse
    {
        public IEnumerable<RecommendationDto> UserInterests { get; set; }
        public IEnumerable<RecommendationDto> RecommendedInterests { get; set; }
    }
}
