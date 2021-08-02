using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class ProfileAcessRepository : IProfileAcessRepository
    {
        public ProfileAcessRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public ProfileAcess Create(long visitorUserId, long visitedUserId)
        {
            var profileAcess = new ProfileAcess
            {
                VisitorUserId = visitorUserId,
                VisitedUserId = visitedUserId,
                VisitDate = DateTime.Now
            };

            _context.ProfileAcess.Add(profileAcess);
            _context.SaveChanges();

            return profileAcess;
        }
    }
}
