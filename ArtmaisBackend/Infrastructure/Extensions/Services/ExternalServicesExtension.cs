using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using ArtmaisBackend.Core.Adresses.Interface;
using ArtmaisBackend.Core.Adresses.Service;
using ArtmaisBackend.Core.Aws.Interface;
using ArtmaisBackend.Core.Aws.Service;
using ArtmaisBackend.Core.Contact.Service;
using ArtmaisBackend.Core.Contacts.Interface;
using ArtmaisBackend.Core.Dashboard.Services;
using ArtmaisBackend.Core.OAuth.Google.Interface;
using ArtmaisBackend.Core.OAuth.Google.Mediator;
using ArtmaisBackend.Core.Payments.Interface;
using ArtmaisBackend.Core.Payments.Service;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Service;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.Profile.Mediator;
using ArtmaisBackend.Core.Publications.Interface;
using ArtmaisBackend.Core.Publications.Service;
using ArtmaisBackend.Core.Recomendation.Services;
using ArtmaisBackend.Core.Signatures.Interface;
using ArtmaisBackend.Core.Signatures.Service;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.SignIn.Service;
using ArtmaisBackend.Core.SignUp.Interface;
using ArtmaisBackend.Core.SignUp.Service;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Core.Users.Service;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Services;
using ArtmaisBackend.Services.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ArtmaisBackend.Infrastructure.Extensions.Services
{
    [ExcludeFromCodeCoverage]
    public static class ExternalServicesExtension
    {
        public static IServiceCollection AddExternalServices(this IServiceCollection services, IConfiguration configuration)
        {
            var awsConfigRegion = configuration.GetSection("AWSConfig:Region").Value;

            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IInterestMediator, InterestMediator>();
            services.AddScoped<IRecomendationMediator, RecommendationMediator>();
            services.AddScoped<IProfileAccessMediator, ProfileAccessMediator>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IGoogleService, GoogleService>();
            services.AddScoped<IGoogleMediator, GoogleMediator>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IAwsService, AwsService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IPublicationService, PublicationService>();
            services.AddScoped<IRecomendationService, RecomendationService>();
            services.AddScoped<ISignatureService, SignatureService>();
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddAWSService<IAmazonS3>(new AWSOptions { Region = RegionEndpoint.GetBySystemName(awsConfigRegion) });

            services.Configure<SocialMediaConfiguration>(configuration.GetSection("SocialMediaShareLink"));
            services.Configure<DbServiceConfiguration>(configuration.GetSection("DbServiceConfig"));
            services.Configure<MercadoPagoConfiguration>(configuration.GetSection("MercadoPagoConfig"));

            services.AddHttpClient();

            return services;
        }
    }
}
