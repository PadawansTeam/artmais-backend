using ArtmaisBackend.Core.Entities;
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
            mockSignatureRepository.Setup(x => x.Create(userId));
            
            var signatureService = new SignatureService(mockSignatureRepository.Object);
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
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);

            var signatureService = new SignatureService(mockSignatureRepository.Object);
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
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);

            var signatureService = new SignatureService(mockSignatureRepository.Object);
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
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);

            var signatureService = new SignatureService(mockSignatureRepository.Object);
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
            mockSignatureRepository.Setup(x => x.GetSignatureByUserId(userId)).ReturnsAsync(signature);

            var signatureService = new SignatureService(mockSignatureRepository.Object);
            var result = await signatureService.GetSignatureByUserId(userId);
            result.Should().BeFalse();
        }
    }
}
