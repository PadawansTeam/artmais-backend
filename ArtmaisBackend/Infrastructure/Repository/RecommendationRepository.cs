using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly ArtplusContext _context;

        public RecommendationRepository(ArtplusContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Recommendation> AddAsync(int interestId, int subcategoryId)
        {
            var recomendation = new Recommendation
            {
                SubcategoryID = subcategoryId,
                InterestID = interestId
            };

            await _context.Recommendation.AddAsync(recomendation);

            await _context.SaveChangesAsync();

            return recomendation;
        }

        public void DeleteAllByUserId(long userId)
        {
            _context.Recommendation.RemoveRange(_context.Recommendation
                .Where(r => r.Interest.UserID.Equals(userId)));

            _context.SaveChanges();
        }

        public IEnumerable<SubcategoryDto> GetSubcategoriesByUserId(long userId)
        {
            var results = (from recommendation in _context.Recommendation
                           join subcategory in this._context.Subcategory on recommendation.SubcategoryID equals subcategory.SubcategoryID
                           where subcategory.OtherSubcategory.Equals(false)
                           && recommendation.Interest.UserID.Equals(userId)
                           select new SubcategoryDto
                           {
                               SubcategoryID = subcategory.SubcategoryID,
                               Subcategory = subcategory.UserSubcategory,
                               Category = subcategory.Category.UserCategory
                           }).Distinct();

            return results;
        }

        public void Delete(Recommendation recommendation)
        {
            _context.Recommendation.Remove(recommendation);

            _context.SaveChanges();
        }

        public Recommendation GetRecommendationByUserIdAndSubcategoryId(long userId, int subcategory)
        {
            return _context.Recommendation
                .Where(r => r.Interest.UserID.Equals(userId) && r.SubcategoryID.Equals(subcategory))
                .FirstOrDefault();
        }
    }
}
