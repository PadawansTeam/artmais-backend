using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.SignIn;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Xunit;

namespace ArtmaisBackend.Tests.Core.SignInTest
{
    public class JwtTokenTest
    {
        [Fact]
        public void GenerateTokenReturnsToken()
        {
            var user = new User
            {
                UserID = 1,
                Email = "joao@gmail.com",
                Password = "05ZqadUMOvuD8CAL+jffYg==awRk+A/eBTdeZu2HHUn5rEkgBtFefv6ljXH4TLoLoD66V1pCKjj7CN/cXMZxINsgGMaHRUxSbOOl5ahWCtPnTQ==",
                Role = "Consumidor"
            };

            var inMemorySettings = new Dictionary<string, string> {
                {"Secret", "b62ff62b9c32a6454ea75d9b8bfebbbb"},
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var jwtToken = new JwtToken(configuration);

            Assert.NotNull(jwtToken.GenerateToken(user));
        }
    }
}
