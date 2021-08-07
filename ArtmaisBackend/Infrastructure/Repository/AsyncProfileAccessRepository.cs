using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class AsyncProfileAccessRepository : IAsyncProfileAccessRepository
    {
        public AsyncProfileAccessRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IEnumerable<CategoryRating> GetAllCategoryRating()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtplusContext>();
            var results = (from profileAccess in context.ProfileAccess
                           join user in context.User on profileAccess.VisitedUserId equals user.UserID
                           group user by profileAccess into grouped
                           select new CategoryRating
                           {
                               VisitorUserId = grouped.Key.VisitorUserId,
                               VisitedSubcategoryId = grouped.Key.VisitedUser.SubcategoryID,
                               VisitNumber = grouped.Count()
                           }).ToList();

            return results;
        }
    }
}
