using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using oauth_poc.Core.SignIn;
using oauth_poc.Core.SignIn.Interface;
using oauth_poc.Core.SignUp;
using oauth_poc.Core.SignUp.Interface;
using oauth_poc.Infrastructure.Data;
using oauth_poc.Infrastructure.Repository;
using oauth_poc.Infrastructure.Repository.Interface;
using System.Text;

namespace oauth_poc
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "oauth_poc", Version = "v1" });
            });

            services.AddDbContext<AuthContext>(
                options => options.UseNpgsql(this.Configuration.GetConnectionString("DbContext")));

            //Repository
            services.AddScoped<IUserRepository, UserRepository>();

            //SignIn
            services.AddScoped<ISignIn, SignIn>();
            services.AddScoped<IJwtToken, JwtToken>();

            //SignUp
            services.AddScoped<ISignUp, SignUp>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "oauth_poc v1"));

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
