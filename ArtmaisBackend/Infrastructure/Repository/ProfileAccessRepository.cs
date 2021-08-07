using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class ProfileAccessRepository : IProfileAccessRepository
    {
        public ProfileAccessRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public ProfileAccess Create(long visitorUserId, long visitedUserId)
        {
            var profileAcess = new ProfileAccess
            {
                VisitorUserId = visitorUserId,
                VisitedUserId = visitedUserId,
                VisitDate = DateTime.Now
            };

            _context.ProfileAccess.Add(profileAcess);
            _context.SaveChanges();

            return profileAcess;
        }
    }
}
