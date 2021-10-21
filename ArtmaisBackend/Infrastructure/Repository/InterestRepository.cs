using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class InterestRepository : IInterestRepository
    {
        public InterestRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public async Task<IEnumerable<Interest>> DeleteAllAndCreateAllAsync(InterestRequest interestRequest, long userId)
        {
            var interests = new List<Interest>();

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                DeleteAll(userId);

                foreach (var subcategoryId in interestRequest.SubcategoryID)
                {
                    interests.Add(await Create(subcategoryId, userId));
                }

                transaction.Commit();
                _context.SaveChanges();
                return interests;
            }
            catch(Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        private async Task<Interest> Create(int subcategoryId, long userId)
        {
            var interest = new Interest
            {
                UserID = userId,
                SubcategoryID = subcategoryId,
                UserSelected = true
            };

            await _context.Interest.AddAsync(interest);

            return interest;
        }

        private void DeleteAll(long userId)
        {
            _context.Interest.RemoveRange(_context.Interest
                .Where(i => i.UserID.Equals(userId) && i.UserSelected));
        }
    }
}
