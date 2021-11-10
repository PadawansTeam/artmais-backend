using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ArtmaisBackend.Infrastructure.Extensions.DatabaseContext
{
    [ExcludeFromCodeCoverage]
    public static class DatabaseContextExtension
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ArtplusContext>(
                options => options.UseNpgsql(configuration["DbContext"]));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategorySubcategoryRepository, CategorySubcategoryRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IExternalAuthorizationRepository, ExternalAuthorizationRepository>();
            services.AddScoped<IProfileAccessRepository, ProfileAccessRepository>();
            services.AddSingleton<IAsyncProfileAccessRepository, AsyncProfileAccessRepository>();
            services.AddSingleton<IAsyncInterestRepository, AsyncInterestRepository>();
            services.AddScoped<IMediaRepository, MediaRepository>();
            services.AddScoped<IMediaTypeRepository, MediaTypeRepository>();
            services.AddScoped<IPublicationRepository, PublicationRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ILikeRepository, LikeRepository>();
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();
            services.AddScoped<ISignatureRepository, SignatureRepository>();

            return services;
        }
    }
}
