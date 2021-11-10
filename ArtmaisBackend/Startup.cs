using ArtmaisBackend.Infrastructure.Extensions.DatabaseContext;
using ArtmaisBackend.Infrastructure.Extensions.Securities;
using ArtmaisBackend.Infrastructure.Extensions.Services;
using ArtmaisBackend.Infrastructure.Extensions.Swagger;
using ArtmaisBackend.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ArtmaisBackend
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup()
        {
            this.Configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddExternalServices(Configuration)
                .AddDatabaseContext(Configuration)
                .AddSwaggerGenService()
                .AddSecuritiesServices(Configuration)
                .AddAutoMapper(typeof(Startup).Assembly)
                .AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArtmaisBackend v1"));

            app.UseHttpsRedirection();
            app.UseMiddleware(typeof(CustomResponseHeaderMiddleware));
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
