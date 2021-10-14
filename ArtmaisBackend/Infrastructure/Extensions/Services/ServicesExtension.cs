﻿using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.S3;
using ArtmaisBackend.Core.Adresses.Interface;
using ArtmaisBackend.Core.Adresses.Service;
using ArtmaisBackend.Core.Aws.Interface;
using ArtmaisBackend.Core.Aws.Service;
using ArtmaisBackend.Core.Contact.Service;
using ArtmaisBackend.Core.Contacts.Interface;
using ArtmaisBackend.Core.OAuth.Google.Interface;
using ArtmaisBackend.Core.OAuth.Google.Mediator;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Service;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.Profile.Mediator;
using ArtmaisBackend.Core.Profile.Services;
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
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            var awsConfigRegion = configuration.GetSection("AWSConfig:Region").Value;

            services.AddHostedService<RecomendationService>();
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IInterestMediator, InterestMediator>();
            services.AddScoped<IRecomendationMediator, RecomendationMediator>();
            services.AddScoped<IProfileAccessMediator, ProfileAccessMediator>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IGoogleService, GoogleService>();
            services.AddScoped<IGoogleMediator, GoogleMediator>();
            services.AddScoped<IPortfolioService, PortfolioService>();
            services.AddScoped<IAwsService, AwsService>();
            services.AddAWSService<IAmazonS3>(new AWSOptions { Region = RegionEndpoint.GetBySystemName(awsConfigRegion) });
            services.Configure<SocialMediaConfiguration>(configuration.GetSection("SocialMediaShareLink"));

            return services;
        }
    }
}
