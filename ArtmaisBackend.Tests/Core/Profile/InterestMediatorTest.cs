﻿using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Profile
{
    public class InterestMediatorTest
    {
        [Fact]
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
            var jwtTokenMock = new Mock<IJwtToken>();
            jwtTokenMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var interestMediator = new InterestMediator(categorySubcategoryRepositoryMock.Object, interestRepositoryMock.Object, jwtTokenMock.Object);
            var result = interestMediator.Index(null);

            Assert.IsAssignableFrom<InterestDto>(result);
        }

        [Fact]
        public void CreateReturnsMessage()
        {
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

            var message = new { message = "Os interesses foram salvos com sucesso." };

            var categorySubcategoryRepositoryMock = new Mock<ICategorySubcategoryRepository>();
            var interestRepositoryMock = new Mock<IInterestRepository>();
            interestRepositoryMock.Setup(r => r.DeleteAllAndCreateAll(request, 1)).Returns(message);
            var jwtTokenMock = new Mock<IJwtToken>();
            jwtTokenMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var interestMediator = new InterestMediator(categorySubcategoryRepositoryMock.Object, interestRepositoryMock.Object, jwtTokenMock.Object);
            var result = interestMediator.Create(request, null);

            Assert.Equal(new { message = "Os interesses foram salvos com sucesso." }, result);
        }
    }
}