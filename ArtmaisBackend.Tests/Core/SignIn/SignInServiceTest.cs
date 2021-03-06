using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.SignIn.Service;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Moq;
using Xunit;

namespace ArtmaisBackend.Tests.Core.SignIn
{
    public class SignInServiceTest
    {
        [Fact(DisplayName = "Authenticate shouldn't be null when encrypt with method")]
        public void AuthenticateReturnsToken()
        {
            var request = new SigInRequest
            {
                Email = "joao@gmail.com",
                Password = "123456789",
            };

            var user = new User
            {
                UserID = 1,
                Email = "joao@gmail.com",
                Password = "05ZqadUMOvuD8CAL+jffYg==awRk+A/eBTdeZu2HHUn5rEkgBtFefv6ljXH4TLoLoD66V1pCKjj7CN/cXMZxINsgGMaHRUxSbOOl5ahWCtPnTQ==",
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetUserByEmail("joao@gmail.com")).Returns(user);

            var jwtTokenMock = new Mock<IJwtTokenService>();
            jwtTokenMock.Setup(j => j.GenerateToken(user)).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ");

            var authenticate = new SignInService(userRepositoryMock.Object, jwtTokenMock.Object);

            Assert.NotNull(authenticate.Authenticate(request));
        }

        [Fact(DisplayName = "Authenticate should throw unauthorized when email is not authenticate")]
        public void AuthenticateThrowsUnauthorizedByEmail()
        {
            var request = new SigInRequest
            {
                Email = "joao@gmail.com",
                Password = "123456789",
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetUserByEmail("joao@gmail.com")).Returns((User)null);

            var jwtTokenMock = new Mock<IJwtTokenService>();

            var authenticate = new SignInService(userRepositoryMock.Object, jwtTokenMock.Object);

            Assert.Throws<Unauthorized>(() => authenticate.Authenticate(request));
        }

        [Fact(DisplayName = "Authenticate should throw unauthorized when password is not authenticate")]
        public void AuthenticateThrowsUnauthorizedByPassword()
        {
            var request = new SigInRequest
            {
                Email = "joao@gmail.com",
                Password = "12345678910",
            };

            var user = new User
            {
                UserID = 1,
                Email = "joao@gmail.com",
                Password = "05ZqadUMOvuD8CAL+jffYg==awRk+A/eBTdeZu2HHUn5rEkgBtFefv6ljXH4TLoLoD66V1pCKjj7CN/cXMZxINsgGMaHRUxSbOOl5ahWCtPnTQ==",
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetUserByEmail("joao@gmail.com")).Returns(user);

            var jwtTokenMock = new Mock<IJwtTokenService>();
            jwtTokenMock.Setup(j => j.GenerateToken(user)).Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ");

            var authenticate = new SignInService(userRepositoryMock.Object, jwtTokenMock.Object);

            Assert.Throws<Unauthorized>(() => authenticate.Authenticate(request));
        }
    }
}
