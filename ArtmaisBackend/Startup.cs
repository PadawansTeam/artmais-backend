using ArtmaisBackend.Core.Adresses.Interface;
using ArtmaisBackend.Core.Adresses.Service;
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
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Services;
using ArtmaisBackend.Services.Interface;
using ArtmaisBackend.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace ArtmaisBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            var key = Encoding.ASCII.GetBytes(this.Configuration.GetValue("Secret", ""));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            }
            );

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAny",
                                  builder =>
                                  {
                                      builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                                  });
            });


            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArtmaisBackend", Version = "v1" });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme." +
                                  "\r\n\r\nEnter 'Bearer'[space] and then your token in the text input below." +
                                  "\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            services.AddDbContext<ArtplusContext>(
                options => options.UseNpgsql(Environment.GetEnvironmentVariable("pgsqldb")));

            //HostedServices
            services.AddHostedService<RecomendationService>();

            //Automapper
            services.AddAutoMapper(typeof(Startup).Assembly);

            //Options
            services.Configure<SocialMediaConfiguration>(Configuration.GetSection("SocialMediaShareLink"));

            //Repository
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

            //SignIn
            services.AddScoped<ISignInService, SignInService>();

            //Profile
            services.AddScoped<IInterestMediator, InterestMediator>();
            services.AddScoped<IRecomendationMediator, RecomendationMediator>();
            services.AddScoped<IProfileAccessMediator, ProfileAccessMediator>();

            //SignUp
            services.AddScoped<ISignUpService, SignUpService>();

            //User
            services.AddScoped<IUserService, UserService>();

            //Contact
            services.AddScoped<IContactService, ContactService>();

            //Address
            services.AddScoped<IAddressService, AddressService>();

            //Google Services
            services.AddScoped<IGoogleService, GoogleService>();

            //OAuth Google
            services.AddScoped<IGoogleMediator, GoogleMediator>();

            //Portfolio
            services.AddScoped<IPortfolioService, PortfolioService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArtmaisBackend v1"));

            app.UseHttpsRedirection();
            app.UseCors("AllowAny");
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
