using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class InterestRepository : IInterestRepository
    {
        public InterestRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public IEnumerable<Interest> Create(InterestRequest interestRequest, int userId)
        {
            var interests = new List<Interest>();

            foreach (var subcategoryId in interestRequest.SubcategoryID)
            {
                var interest = new Interest
                {
                    UserID = userId,
                    SubcategoryID = subcategoryId
                };

                interests.Add(interest);
                _context.Interest.Add(interest);
                _context.SaveChanges();
            }

            return interests;
        }
    }
}
