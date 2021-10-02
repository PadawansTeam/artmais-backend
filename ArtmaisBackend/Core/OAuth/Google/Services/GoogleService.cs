using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Services.Interface;
using Google.Apis.Auth;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ArtmaisBackend.Services
{
    public class GoogleService : IGoogleService
    {
        public GoogleService(IOptions<GoogleConfiguration> options)
        {
            _configuration = options.Value;
        }

        private readonly GoogleConfiguration _configuration;

        public async Task<GoogleJsonWebSignature.Payload> ValidateToken(string token)
        {
            var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { _configuration.ClientId }
            });

            return googleUser;
        }
    }
}
