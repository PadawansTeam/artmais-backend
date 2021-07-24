using ArtmaisBackend.Core.Adresses.Interface;
using ArtmaisBackend.Core.Adresses.Service;
using ArtmaisBackend.Core.Contact.Service;
using ArtmaisBackend.Core.Contacts.Interface;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.Profile.Mediator;
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
            this.Configuration = configuration;
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


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ArtmaisBackend", Version = "v1" });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Bearer Token",

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
                options => options.UseNpgsql(this.Configuration.GetConnectionString("DbContext")));

            //Automapper
            services.AddAutoMapper(typeof(Startup).Assembly);

            //Options
            services.Configure<SocialMediaConfiguration>(this.Configuration.GetSection("SocialMediaShareLink"));

            //Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategorySubcategoryRepository, CategorySubcategoryRepository>();
            services.AddScoped<IInterestRepository, InterestRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();

            //SignIn
            services.AddScoped<ISignInService, SignInService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            //Profile
            services.AddScoped<IInterestMediator, InterestMediator>();
            services.AddScoped<IRecomendationMediator, RecomendationMediator>();

            //SignUp
            services.AddScoped<ISignUpService, SignUpService>();

            //User
            services.AddScoped<IUserService, UserService>();

            //Contact
            services.AddScoped<IContactService, ContactService>();

            //Address
            services.AddScoped<IAddressService, AddressService>();
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
