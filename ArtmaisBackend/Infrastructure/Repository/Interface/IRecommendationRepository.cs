using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IRecommendationRepository
    {
        Task<Recommendation> AddAsync(int interestId, int subcategoryId);
        void DeleteAllByUserId(long userId);
        IEnumerable<SubcategoryDto> GetSubcategoriesByUserId(long userId);
        void DeleteByUserIdAndSubcategoryId(long userId, int subcategory);
    }
}
