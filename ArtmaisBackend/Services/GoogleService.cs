using ArtmaisBackend.Services.Interface;
using Google.Apis.Auth;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Services
{
    public class GoogleService : IGoogleService
    {
        public async Task<GoogleJsonWebSignature.Payload> ValidateToken(string token)
        {
            var googleUser = await GoogleJsonWebSignature.ValidateAsync(token, new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { Environment.GetEnvironmentVariable("googleclientid") }
            });

            return googleUser;
        }
    }
}
