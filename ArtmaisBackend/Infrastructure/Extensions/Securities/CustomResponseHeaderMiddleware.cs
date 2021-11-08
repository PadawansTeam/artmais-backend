using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Extensions.Securities
{
    [ExcludeFromCodeCoverage]
    public class CustomResponseHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomResponseHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(state =>
            {
                var httpContext = (HttpContext)state;
                httpContext.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
                httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                httpContext.Response.Headers.Add("X-Frame-Options", "DENY");
                httpContext.Response.Headers.Add("Referrer-Policy", "no-referrer");
                return Task.CompletedTask;
            }, context);

            await _next(context);
        }
    }
}
