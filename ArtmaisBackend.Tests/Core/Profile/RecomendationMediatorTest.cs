using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Mediator;
using ArtmaisBackend.Core.Profile.Responses;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Profile
{
    public class RecomendationMediatorTest
    {
        [Fact(DisplayName = "Index should be validate GetUsersByInterest method and returns RecomendationDto")]
        public void IndexReturnsRecomendationDto()
        {
            var recomendations = new List<RecommendationDto>
            {
                new RecommendationDto
                {
                    Username = "Joao",
                    Name = "Name",
                    UserPicture = "",
                    BackgroundPicture = "",
                    Category = "Tatuador(a)",
                    Subcategory = "Aquarela"
                },
                new RecommendationDto
                {
                    Username = "Gabriela",
                    Name = "OtherName",
                    UserPicture = "",
                    BackgroundPicture = "",
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
            var jwtTokenMock = new Mock<IJwtTokenService>();
            jwtTokenMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var recomendationMediator = new RecommendationMediator(userRepositoryMock.Object, jwtTokenMock.Object);
            var result = recomendationMediator.Index(null);

            result.Should().BeAssignableTo<RecommendationResponse>();
        }
    }
}
