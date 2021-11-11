using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Mediator;
using ArtmaisBackend.Core.Recomendation.Responses;
using ArtmaisBackend.Core.Recomendation.Services;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Profile
{
    public class InterestMediatorTest
    {
        [Fact(DisplayName = "Index should be validate GetUsersByInterest method and returns SubcategoryDto")]
        public void IndexReturnsSubcategoryDto()
        {
            var subcategories = new List<SubcategoryDto>
            {
                new SubcategoryDto { SubcategoryID = 1, Subcategory = "Aquarela" },
                new SubcategoryDto { SubcategoryID = 2, Subcategory = "Black Work"}
            };

            var userJwtData = new UserJwtData
            {
                UserID = 1,
                Role = "artist"
            };

            var categorySubcategoryRepositoryMock = new Mock<ICategorySubcategoryRepository>();
            categorySubcategoryRepositoryMock.Setup(r => r.GetSubcategory()).Returns(subcategories);
            categorySubcategoryRepositoryMock.Setup(r => r.GetSubcategoryByInterestAndUserId(1)).Returns(subcategories);

            var interestRepositoryMock = new Mock<IInterestRepository>();
            var jwtTokenMock = new Mock<IJwtTokenService>();
            jwtTokenMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var recomendationServiceMock = new Mock<IRecomendationService>();

            var recomendationRepositoryMock = new Mock<IRecommendationRepository>();

            var loggerMock = new Mock<ILogger<InterestMediator>>();

            var interestMediator = new InterestMediator(categorySubcategoryRepositoryMock.Object,
                interestRepositoryMock.Object,
                jwtTokenMock.Object,
                recomendationServiceMock.Object,
                recomendationRepositoryMock.Object,
                loggerMock.Object);

            var result = interestMediator.Index(null);

            result.Should().BeOfType<InterestDto>();
        }

        [Fact(DisplayName = "Create should be returns success message when InterestRequest it is save")]
        public async Task CreateReturnsSuccessMessage()
        {
            var recomendation = new Recommendation
            {
                RecomendationID = 1,
                InterestID = 1,
                SubcategoryID = 1
            };

            var recomendationResponse = new RecomendationResponse
            {
                RecommendedSubcategories = new List<int> { 1, 2 }
            };

            var interestList = new List<Interest>
            {
                new Interest
                {
                    InterestID = 1,
                    UserID = 1,
                    User = new User(),
                    SubcategoryID = 1,
                    Subcategory = new Subcategory(),
                    UserSelected = false
                }
            };

            var request = new InterestRequest
            {
                SubcategoryID = new List<int>
                {
                    1,
                    2
                },
                RecommendedSubcategoryID = new List<int>()
            };

            var userJwtData = new UserJwtData
            {
                UserID = 1,
                Role = "artist"
            };

            var categorySubcategoryRepositoryMock = new Mock<ICategorySubcategoryRepository>();
           
            var interestRepositoryMock = new Mock<IInterestRepository>();
            interestRepositoryMock.Setup(r => r.DeleteAllAndCreateAllAsync(request, 1)).ReturnsAsync(interestList);
            
            var jwtTokenMock = new Mock<IJwtTokenService>();
            jwtTokenMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var recomendationServiceMock = new Mock<IRecomendationService>();
            recomendationServiceMock.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(recomendationResponse);

            var recomendationRepositoryMock = new Mock<IRecommendationRepository>();
            recomendationRepositoryMock.Setup(r => r.AddAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(recomendation);

            var loggerMock = new Mock<ILogger<InterestMediator>>();

            var interestMediator = new InterestMediator(categorySubcategoryRepositoryMock.Object,
                interestRepositoryMock.Object,
                jwtTokenMock.Object,
                recomendationServiceMock.Object,
                recomendationRepositoryMock.Object,
                loggerMock.Object);

            var result = await interestMediator.Create(request, null);

            result.Message.Should().Be("Os interesses foram salvos com sucesso.");
        }

        [Fact(DisplayName = "Create should return fail message when an exception occurs")]
        public async Task CreateShouldReturnFailMessageWhenAnExceptionOccurs()
        {
            var recomendationResponse = new RecomendationResponse
            {
                RecommendedSubcategories = new List<int> { 1, 2 }
            };

            var interestList = new List<Interest>
            {
                new Interest
                {
                    InterestID = 1,
                    UserID = 1,
                    User = new User(),
                    SubcategoryID = 1,
                    Subcategory = new Subcategory(),
                    UserSelected = false
                }
            };

            var request = new InterestRequest
            {
                SubcategoryID = new List<int>
                {
                    1,
                    2
                }
            };

            var userJwtData = new UserJwtData
            {
                UserID = 1,
                Role = "artist"
            };

            var categorySubcategoryRepositoryMock = new Mock<ICategorySubcategoryRepository>();

            var interestRepositoryMock = new Mock<IInterestRepository>();
            interestRepositoryMock.Setup(r => r.DeleteAllAndCreateAllAsync(request, 1)).ReturnsAsync(interestList);

            var jwtTokenMock = new Mock<IJwtTokenService>();
            jwtTokenMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var recomendationServiceMock = new Mock<IRecomendationService>();

            var recomendationRepositoryMock = new Mock<IRecommendationRepository>();

            var loggerMock = new Mock<ILogger<InterestMediator>>();

            var interestMediator = new InterestMediator(categorySubcategoryRepositoryMock.Object,
                interestRepositoryMock.Object,
                jwtTokenMock.Object,
                recomendationServiceMock.Object,
                recomendationRepositoryMock.Object,
                loggerMock.Object);

            var result = await interestMediator.Create(request, null);

            result.Message.Should().Be("Erro ao salvar interesses.");
        }
    }
}
