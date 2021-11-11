using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Signatures.Dto;
using ArtmaisBackend.Core.Signatures.Interface;
using ArtmaisBackend.Core.Signatures.Service;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Signatures
{
    public class SignatureServiceTest
    {
        [Fact(DisplayName = "Create Signature Should Be Not Throw")]
        public async Task CreateSignatureShouldBeNotThrow()
        {
            var userId = 3;
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockSignatureRepository.Setup(x => x.Create(userId));

            var signatureService = new SignatureService(mockSignatureRepository.Object, mockUserRepository.Object);
            await signatureService.CreateSignature(userId);
        }

        [Fact(DisplayName = "Update Signature Should Be Not Throw")]
        public async Task UpdateSignatureShouldBeNotThrow()
        {
            var userId = 3;
            var date = DateTime.UtcNow;
            var signature = new Signature
            {
                SignatureID = 1,
                UserID = 3,
                StartDate = date,
                EndDate = date.AddYears(1)
            };
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);

            var signatureService = new SignatureService(mockSignatureRepository.Object, mockUserRepository.Object);
            await signatureService.UpdateSignature(userId);
        }

        [Fact(DisplayName = "Get Signature By User Id Should Be True")]
        public async Task GetSignatureByUserIdShouldBeTrue()
        {
            var userId = 3;
            var date = DateTime.UtcNow;
            var signature = new Signature
            {
                SignatureID = 1,
                UserID = 3,
                StartDate = date,
                EndDate = date.AddYears(1)
            };
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);

            var signatureService = new SignatureService(mockSignatureRepository.Object, mockUserRepository.Object);
            var result = await signatureService.GetSignatureByUserId(userId);
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Get Signature By User Id Should Be False")]
        public async Task GetSignatureByUserIdShouldBeFalse()
        {
            var userId = 3;
            var date = DateTime.UtcNow;
            Signature signature = null;
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);

            var signatureService = new SignatureService(mockSignatureRepository.Object, mockUserRepository.Object);
            var result = await signatureService.GetSignatureByUserId(userId);
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Get Signature By User Id Should Be False when signature ends")]
        public async Task GetSignatureByUserIdShouldBeFalseWhenSignatureEnds()
        {
            var userId = 3;
            var date = DateTime.UtcNow.AddYears(-1);
            var signature = new Signature
            {
                SignatureID = 1,
                UserID = 3,
                StartDate = date,
                EndDate = date.AddYears(1).AddDays(-1)
            };
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);

            var signatureService = new SignatureService(mockSignatureRepository.Object, mockUserRepository.Object);
            var result = await signatureService.GetSignatureByUserId(userId);
            result.Should().BeFalse();
        }

        [Fact(DisplayName = "Get Signature By User Id Should Be Returns Info About Not Premium User")]
        public async Task GetSignatureUserDtoShouldBeReturnDtoUserNotPremium()
        {
            var userId = 3;
            Signature signature = null;
            var user = new User
            {
                UserID = userId,
                Name = "name"
            };
            var expectedResult = new SignatureDto
            {
                UserId = 3,
                Name = "name",
                EndDate = DateTime.MinValue,
                IsPremium = false
            };
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockSignatureService = new Mock<ISignatureService>();
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockUserRepository.Setup(x => x.GetUserById(userId)).Returns(user);
            mockSignatureService.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(false);

            var signatureService = new SignatureService(mockSignatureRepository.Object, mockUserRepository.Object);
            var result = await signatureService.GetSignatureUserDto(userId);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact(DisplayName = "Get Signature Dto By User Id Should Be Returns Info About Not Premium User")]
        public async Task GetSignatureUserDtoShouldBeReturnDtoUserPremium()
        {
            var userId = 3;
            var date = DateTime.UtcNow;
            var user = new User
            {
                UserID = userId,
                Name = "name"
            };
            var signature = new Signature
            {
                SignatureID = 1,
                UserID = user.UserID,
                StartDate = date,
                EndDate = date.AddYears(1).AddDays(-1)
            };
            var expectedResult = new SignatureDto
            {
                UserId = 3,
                Name = "name",
                EndDate = signature.EndDate,
                IsPremium = true
            };
            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);
            mockUserRepository.Setup(x => x.GetUserById(userId)).Returns(user);

            var signatureService = new SignatureService(mockSignatureRepository.Object, mockUserRepository.Object);

            var result = await signatureService.GetSignatureUserDto(userId);
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact(DisplayName = "Get Signature Dto By User Id Should Be throw when user is null")]
        public async Task GetSignatureUserDtoShouldBeThrow()
        {
            var date = DateTime.UtcNow.AddYears(-1);
            User user = null;
            Signature signature = null;

            var mockSignatureRepository = new Mock<ISignatureRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(It.IsAny<long>())).ReturnsAsync(signature);
            mockUserRepository.Setup(x => x.GetUserById(It.IsAny<long>())).Returns(user);

            var signatureService = new SignatureService(mockSignatureRepository.Object, mockUserRepository.Object);

            Func<Task> result = async () =>
            {
                await signatureService.GetSignatureUserDto(It.IsAny<long>());
            };
            await result.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null.");
        }
    }
}
