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
            var results = context.ProfileAccess.Select(p => new { p.VisitorUserId, p.VisitedUser.SubcategoryID }).ToList();

            var groupedResults = results.GroupBy(p => p.VisitorUserId)
                .Select(p => new CategoryRating 
                { 
                    VisitorUserId = p.Key,
                    VisitedSubcategoryId = p.Select(p => p.SubcategoryID).First(),
                    VisitNumber = p.Select(p => p.SubcategoryID).Count()
                });

            return groupedResults;
        }
    }
}
