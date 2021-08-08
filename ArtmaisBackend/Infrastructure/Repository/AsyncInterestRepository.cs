using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class AsyncInterestRepository : IAsyncInterestRepository
    {
        public AsyncInterestRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public void Create(CategoryRating categoryRating)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtplusContext>();

            var interest = new Interest
            {
                UserID = categoryRating.VisitorUserId,
                SubcategoryID = categoryRating.VisitedSubcategoryId,
                UserSelected = false
            };
            context.Interest.Add(interest);
            context.SaveChanges();
        }
    }
}
