using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class RecomendationRepository : IRecomendationRepository
    {
        private readonly ArtplusContext _context;

        public RecomendationRepository(ArtplusContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Recomendation> AddAsync(int interestId, int subcategoryId)
        {
            var recomendation = new Recomendation
            {
                SubcategoryID = subcategoryId,
                InterestID = interestId
            };

            await _context.Recomendation.AddAsync(recomendation);

            await _context.SaveChangesAsync();

            return recomendation;
        }

        public void DeleteAllByUserId(long userId)
        {
            _context.Recomendation.RemoveRange(_context.Recomendation
                .Where(r => r.Interest.UserID.Equals(userId)));

            _context.SaveChanges();
        }
    }
}
