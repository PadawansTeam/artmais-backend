using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Diagnostics.CodeAnalysis;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
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
                VisitDate = DateTime.UtcNow
            };

            _context.ProfileAccess.Add(profileAcess);
            _context.SaveChanges();

            return profileAcess;
        }
    }
}
