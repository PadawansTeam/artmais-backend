using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

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

        public bool GetInterestByUserIdAndSubcategoryId(CategoryRating categoryRating)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtplusContext>();

            return context.Interest
                .Where(i => i.UserID.Equals(categoryRating.VisitorUserId) 
                && i.SubcategoryID.Equals(categoryRating.VisitedSubcategoryId))
                .Any();
        }
    }
}
