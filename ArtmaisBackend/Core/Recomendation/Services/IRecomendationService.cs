using ArtmaisBackend.Core.Recomendation.Responses;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Recomendation.Services
{
    public interface IRecomendationService
    {
        Task<RecomendationResponse> GetAsync(int subcategory);
    }
}
