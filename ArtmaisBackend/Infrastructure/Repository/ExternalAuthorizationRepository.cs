using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class ExternalAuthorizationRepository : IExternalAuthorizationRepository
    {
        public ExternalAuthorizationRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public ExternalAuthorization Create(string externalAuthorizationId, long userId)
        {
            var externalAuthorization = new ExternalAuthorization
            {
                ExternalAuthorizationId = externalAuthorizationId,
                UserId = userId
            };

            _context.ExternalAuthorization.Add(externalAuthorization);
            _context.SaveChanges();

            return externalAuthorization;
        }

        public ExternalAuthorization GetExternalAuthorizationByExternalAuthorizationId(string externalAuthorizationId)
        {
            return _context.ExternalAuthorization.Where(e => e.ExternalAuthorizationId.Equals(externalAuthorizationId)).FirstOrDefault();
        }
    }
}
