using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Mediator;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Profile
{
    public class RecomendationMediatorTest
    {
        [Fact]
        public void IndexReturnsRecomendationDto()
        {
            var recomendations = new List<RecomendationDto>
            {
                new RecomendationDto
                {
                    Username = "Joao",
                    UserPicture = "",
                    Category = "Tatuador(a)",
                    Subcategory = "Aquarela"
                },
                new RecomendationDto
                {
                    Username = "Gabriela",
                    UserPicture = "",
                    Category = "Tatuador(a)",
                    Subcategory = "Black Work"
                }
            };

            var userJwtData = new UserJwtData
            {
                UserID = 1,
                Role = "artist"
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetUsersByInterest(1)).Returns(recomendations);
            var jwtTokenMock = new Mock<IJwtToken>();
            jwtTokenMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var recomendationMediator = new RecomendationMediator(userRepositoryMock.Object, jwtTokenMock.Object);
            var result = recomendationMediator.Index(null);

            Assert.IsAssignableFrom<IEnumerable<RecomendationDto>>(result);
        }
    }
}
