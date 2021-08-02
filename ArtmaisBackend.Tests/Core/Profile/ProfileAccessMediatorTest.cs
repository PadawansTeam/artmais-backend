using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Mediator;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Profile
{
    public class ProfileAccessMediatorTest
    {
        [Fact(DisplayName = "Create returns ProfileAcess")]
        public void CreateReturnsProfileAcess()
        {
            var profileAcess = new ProfileAcess
            {
                ProfileAcessId = 1,
                VisitorUserId = 1,
                VisitedUserId = 2,
                VisitDate = DateTime.Now
            };

            var userJwtData = new UserJwtData
            {
                UserID = 1,
                Role = "artist",
                UserName = "carlos"
            };

            var jwtTokenServiceMock = new Mock<IJwtTokenService>();
            jwtTokenServiceMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var profileAcessRepositoryMock = new Mock<IProfileAccessRepository>();
            profileAcessRepositoryMock.Setup(p => p.Create(1, 2)).Returns(profileAcess);

            var profileAcessMediator = new ProfileAccessMediator(jwtTokenServiceMock.Object, profileAcessRepositoryMock.Object);
            var result = profileAcessMediator.Create(null, 2);

            result.ProfileAcessId.Should().Be(1);
            result.VisitorUserId.Should().Be(1);
            result.VisitedUserId.Should().Be(2);
        }

        [Fact(DisplayName = "Create returns null")]
        public void CreateReturnsNull()
        {
            var userJwtData = new UserJwtData
            {
                UserID = 1,
                Role = "artist",
                UserName = "carlos"
            };

            var jwtTokenServiceMock = new Mock<IJwtTokenService>();
            jwtTokenServiceMock.Setup(j => j.ReadToken(null)).Returns(userJwtData);

            var profileAcessRepositoryMock = new Mock<IProfileAccessRepository>();
            profileAcessRepositoryMock.Setup(p => p.Create(1, 1)).Returns((ProfileAcess)null);

            var profileAcessMediator = new ProfileAccessMediator(jwtTokenServiceMock.Object, profileAcessRepositoryMock.Object);
            var result = profileAcessMediator.Create(null, 1);

            result.Should().BeNull();
        }
    }
}
