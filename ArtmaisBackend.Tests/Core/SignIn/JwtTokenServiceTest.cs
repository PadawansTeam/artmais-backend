using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.SignIn.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Xunit;

namespace ArtmaisBackend.Tests.Core.SignIn
{
    public class JwtTokenServiceTest
    {
        [Fact(DisplayName = "Should be validate jwt token it is not null when the token is generate by method")]
        public void GenerateTokenReturnsToken()
        {
            var user = new User
            {
                UserID = 1,
                Email = "joao@gmail.com",
                Password = "05ZqadUMOvuD8CAL+jffYg==awRk+A/eBTdeZu2HHUn5rEkgBtFefv6ljXH4TLoLoD66V1pCKjj7CN/cXMZxINsgGMaHRUxSbOOl5ahWCtPnTQ==",
                Role = "artist",
                Username = "joaoartista"
            };

            var inMemorySettings = new Dictionary<string, string> {
                {"Secret", "b62ff62b9c32a6454ea75d9b8bfebbbb"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var jwtToken = new JwtTokenService(configuration);

            Assert.NotNull(jwtToken.GenerateToken(user));
        }

        [Fact(DisplayName = "Should be Returns User Jwt Data with Read Token Method")]
        public void ReadTokenReturnsUserJwtData()
        {
            var user = new User
            {
                UserID = 1,
                Email = "joao@gmail.com",
                Password = "05ZqadUMOvuD8CAL+jffYg==awRk+A/eBTdeZu2HHUn5rEkgBtFefv6ljXH4TLoLoD66V1pCKjj7CN/cXMZxINsgGMaHRUxSbOOl5ahWCtPnTQ==",
                Role = "artist",
                Username = "joaoartista"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var inMemorySettings = new Dictionary<string, string> {
                {"Secret", "b62ff62b9c32a6454ea75d9b8bfebbbb"},
            };

            var key = Encoding.ASCII.GetBytes(inMemorySettings.GetValueOrDefault("Secret"));
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var jwtToken = new JwtTokenService(configuration);
            var token = jwtToken.GenerateToken(user);

            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claims = tokenHandler.ValidateToken(token, validations, out var _);
            var result = jwtToken.ReadToken(claims);

            Assert.Equal(1, result.UserID);
            Assert.Equal("artist", result.Role);
            Assert.Equal("joaoartista", result.UserName);
        }
    }
}
