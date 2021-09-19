using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.SignUp.Dto;
using ArtmaisBackend.Core.SignUp.Request;
using ArtmaisBackend.Core.SignUp.Service;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArtmaisBackend.Tests.Core.SignUp
{
    public class SignUpServiceTest
    {
        [Fact(DisplayName = "Index should be validate GetCategoryAndSubcategory method returns a list of category subcategory")]
        public void IndexReturnsCategorySubcategoryDto()
        {
            var categorySubcategory = new List<CategorySubcategoryDto>
            {
                new CategorySubcategoryDto
                {
                    Category = "Tatuador(a)",
                    Subcategory = new List<string> { "Aquarela", "Blackwork" }
                },
                new CategorySubcategoryDto
                {
                    Category = "Artista Plástico(a)",
                    Subcategory = new List<string> { "Pintura", "Escultura" }
                }
            };

            var userRepositoryMock = new Mock<IUserRepository>();

            var categorySubcategoryRepositoryMock = new Mock<ICategorySubcategoryRepository>();
            categorySubcategoryRepositoryMock.Setup(c => c.GetCategoryAndSubcategory()).Returns(categorySubcategory);

            var signUp = new SignUpService(userRepositoryMock.Object, categorySubcategoryRepositoryMock.Object);
            var result = signUp.Index();

            Assert.IsAssignableFrom<IEnumerable<CategorySubcategoryDto>>(result);
        }

        [Fact(DisplayName = "Create should be throw when email already exist")]
        public void CreateThrowsEmailAlreadyInUse()
        {
            var request = new SignUpRequest
            {
                Name = "Joao",
                Email = "joao@gmail.com",
                Password = "123456789",
                Description = "Apenas um consumidor",
                Username = "Joao",
                BirthDate = DateTime.Now,
                Category = "Tatuador(a)",
                Subcategory = "Aquarela"
            };

            var user = new User
            {
                UserID = 1,
                Email = "joao@gmail.com",
                Password = "05ZqadUMOvuD8CAL+jffYg==awRk+A/eBTdeZu2HHUn5rEkgBtFefv6ljXH4TLoLoD66V1pCKjj7CN/cXMZxINsgGMaHRUxSbOOl5ahWCtPnTQ==",
                UserType = new UserType { UserTypeId = 1, Description = "Artist" },
                UserTypeId = 1
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetUserByEmail("joao@gmail.com")).Returns(user);

            var categorySubcategoryRepositoryMock = new Mock<ICategorySubcategoryRepository>();

            var signup = new SignUpService(userRepositoryMock.Object, categorySubcategoryRepositoryMock.Object);

            Assert.Throws<EmailAlreadyInUse>(() => signup.Create(request));
        }

        [Fact(DisplayName = "Create throws when username already in use")]
        public void CreateThrowsUsernameAlreadyInUse()
        {
            var request = new SignUpRequest
            {
                Name = "Joao",
                Email = "joao@gmail.com",
                Password = "123456789",
                Description = "Apenas um consumidor",
                Username = "Joao",
                BirthDate = DateTime.Now,
                Category = "Tatuador(a)",
                Subcategory = "Aquarela"
            };

            var user = new User
            {
                UserID = 1,
                Email = "joao@gmail.com",
                Password = "05ZqadUMOvuD8CAL+jffYg==awRk+A/eBTdeZu2HHUn5rEkgBtFefv6ljXH4TLoLoD66V1pCKjj7CN/cXMZxINsgGMaHRUxSbOOl5ahWCtPnTQ==",
                UserType = new UserType { UserTypeId = 1, Description = "Artist" },
                UserTypeId = 1
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetUserByEmail("joao@gmail.com")).Returns((User)null);
            userRepositoryMock.Setup(r => r.GetUserByUsername("Joao")).Returns(user);

            var categorySubcategoryRepositoryMock = new Mock<ICategorySubcategoryRepository>();

            var signup = new SignUpService(userRepositoryMock.Object, categorySubcategoryRepositoryMock.Object);

            Assert.Throws<UsernameAlreadyInUse>(() => signup.Create(request));
        }
    }
}
