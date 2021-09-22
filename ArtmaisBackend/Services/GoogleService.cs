using ArtmaisBackend.Services.Interface;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ArtmaisBackend.Services
{
    public class GoogleService : IGoogleService
    {
        public GoogleService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public async Task<GoogleJsonWebSignature.Payload> ValidateToken(string token)
        {
            var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { _configuration.GetConnectionString("GoogleClientId") }
            });

            return googleUser;
        }
    }
}
