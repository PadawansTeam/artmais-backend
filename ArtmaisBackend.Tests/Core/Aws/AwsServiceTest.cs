using Amazon.S3;
using Amazon.S3.Model;
using ArtmaisBackend.Core.Aws;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Request;
using ArtmaisBackend.Core.Aws.Service;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Aws
{
    public class AwsServiceTest
    {
        private const string BUCKET_NAME = "bucket-name";
        private const string FILE_PATH = "file-path-name/";
        private static readonly string objectKey = "objectKey-name";
        private static readonly S3CannedACL fileCannedACL = S3CannedACL.PublicRead;

        [Fact(DisplayName = "WritingAnObjectAsync should not be null and update database")]
        public async Task WritingAnObjectAsyncShouldNotBeNull()
        {
            #region Mocks
            var descriptionRequest = new AwsDto("UserPicture");
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPortfolioService = new Mock<IPortfolioService>();
            var mockMapper = new Mock<IMapper>();
            var fileMock = new Mock<IFormFile>();
            var mockS3Client = new Mock<IAmazonS3>();

            var keyName = FILE_PATH + 3 + "/" + objectKey + ".png";
            var content = "UserPicture";
            var fileName = "UserPicture.png";
            var fileContent = "image/png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.ContentType).Returns(fileContent);
            var file = fileMock.Object;

            var userInfo = new User
            {
                UserID = 3,
                UserPicture = "oldUserPicture",
            };
            var updatedUser = new User
            {
                UserID = 3,
                UserPicture = "newUserPicture",
            };
            var putRequest = new PutObjectRequest
            {
                BucketName = BUCKET_NAME,
                Key = keyName,
                InputStream = ms,
                ContentType = file.ContentType,
                CannedACL = fileCannedACL
            };
            var putResponse = new PutObjectResponse { };

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<int>()))).Returns(userInfo);
            mockMapper.Setup(m => m.Map(descriptionRequest, userInfo)).Returns(updatedUser);
            mockUserRepository.Setup(x => x.Update((It.IsAny<User>()))).Returns(updatedUser);
            mockS3Client.Setup(x => x.PutObjectAsync(putRequest, It.IsAny<CancellationToken>())).ReturnsAsync(putResponse);
            #endregion

            var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object, mockS3Client.Object, mockPortfolioService.Object);

            var result = await awsService.WritingAnObjectAsync(new UploadObjectCommand
            {
                File = file,
                BucketName = putRequest.BucketName
            }
            );

            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "WritingAnObjectAsync should be false when request is null or empty")]
        public async Task WritingAnObjectAsyncShouldBeFalse()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPortfolioService = new Mock<IPortfolioService>();
            var mockMapper = new Mock<IMapper>();
            var mockS3Client = new Mock<IAmazonS3>();
            var fileMock = new Mock<IFormFile>();
            var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object, mockS3Client.Object, mockPortfolioService.Object);
            var file = fileMock.Object;
            mockS3Client.Setup(x => x.PutObjectAsync(It.IsAny<PutObjectRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new AmazonS3Exception(string.Empty));


            Func<Task> result = async () =>
            {
                await awsService.WritingAnObjectAsync(new UploadObjectCommand
                {
                    File = file,
                    BucketName = BUCKET_NAME
                });
            };
            await result.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact(DisplayName = "UploadObjectAsync should not be null and update database")]
        public async Task UploadObjectAsyncShouldNotBeNullWithProfilePicture()
        {
            #region Mocks
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPortfolioService = new Mock<IPortfolioService>();
            var mockMapper = new Mock<IMapper>();
            var fileMock = new Mock<IFormFile>();
            var mockS3Client = new Mock<IAmazonS3>();
            var content = "UserPicture";
            var fileName = "UserPicture.png";
            var fileContent = "image/png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.ContentType).Returns(fileContent);
            var file = fileMock.Object;

            var uploadObjectCommand = UploadObjectCommandFactory.Create(3, file, Channel.PROFILE);
            #endregion

            var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object, mockS3Client.Object, mockPortfolioService.Object);

            var result = await awsService.UploadObjectAsync(uploadObjectCommand);

            result.Content.Should().NotBeNull();
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "UploadObjectAsync should not be null and update database")]
        public async Task UploadObjectAsyncShouldNotBeNullWithPortfolioContent()
        {
            #region Mocks
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPortfolioService = new Mock<IPortfolioService>();
            var mockMapper = new Mock<IMapper>();
            var fileMock = new Mock<IFormFile>();
            var mockS3Client = new Mock<IAmazonS3>();
            var content = "UserPicture";
            var fileName = "UserPicture.png";
            var fileContent = "image/png";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            fileMock.Setup(_ => _.ContentType).Returns(fileContent);
            var file = fileMock.Object;

            var uploadObjectCommand = UploadObjectCommandFactory.Create(3, file, Channel.PROFILE);
            #endregion

            var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object, mockS3Client.Object, mockPortfolioService.Object);

            var result = await awsService.UploadObjectAsync(uploadObjectCommand);

            result.Content.Should().NotBeNull();
            result.Should().NotBeNull();
        }

        [Fact(DisplayName = "DeletingAnObjectAsync should be true with https and update database")]
        public async Task DeletingAnObjectAsyncShouldNotBeTrueWithHttps()
        {
            #region Mocks
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPortfolioService = new Mock<IPortfolioService>();
            var mockMapper = new Mock<IMapper>();
            var mockS3Client = new Mock<IAmazonS3>();
            var userId = 112;
            var publicationId = 12;
            var deleteObjectComand = new DeleteObjectCommand(userId, publicationId);
            var portfolioContent = new PortfolioContentDto
            {
                UserID = 112,
                PublicationID = 12,
                MediaID = 1,
                MediaTypeID = 2,
                S3UrlMedia = "https:///bucket-artmais.s3.amazonaws.com/portfolio-contents/3/S3UrlMedia1.jpg",
                Description = "Description1",
                PublicationDate = new DateTime()
            };
            mockPortfolioService.Setup(p => p.GetPublicationByIdToDelete(It.IsAny<int>(), It.IsAny<long>())).Returns(portfolioContent);
            var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object, mockS3Client.Object, mockPortfolioService.Object);
            #endregion

            var result = await awsService.DeletingAnObjectAsync(deleteObjectComand);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "DeletingAnObjectAsync should be true with http and update database")]
        public async Task DeletingAnObjectAsyncShouldNotBeTrueWithHttp()
        {
            #region Mocks
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPortfolioService = new Mock<IPortfolioService>();
            var mockMapper = new Mock<IMapper>();
            var mockS3Client = new Mock<IAmazonS3>();
            var userId = 112;
            var publicationId = 12;
            var deleteObjectComand = new DeleteObjectCommand(userId, publicationId);
            var portfolioContent = new PortfolioContentDto
            {
                UserID = 112,
                PublicationID = 12,
                MediaID = 1,
                MediaTypeID = 2,
                S3UrlMedia = "http:///bucket-artmais.s3.amazonaws.com/portfolio-contents/3/S3UrlMedia1.jpg",
                Description = "Description1",
                PublicationDate = new DateTime()
            };
            mockPortfolioService.Setup(p => p.GetPublicationByIdToDelete(It.IsAny<int>(), It.IsAny<long>())).Returns(portfolioContent);
            var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object, mockS3Client.Object, mockPortfolioService.Object);
            #endregion

            var result = await awsService.DeletingAnObjectAsync(deleteObjectComand);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "DeletingAnObjectAsync should be throw AmazonS3Exception")]
        public async Task DeletingAnObjectAsyncShouldBeThrowAmazonS3Exception()
        {
            #region Mocks
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPortfolioService = new Mock<IPortfolioService>();
            var mockMapper = new Mock<IMapper>();
            var fileMock = new Mock<IFormFile>();
            var mockS3Client = new Mock<IAmazonS3>();
            var portfolioContent = new PortfolioContentDto
            {
                UserID = 112,
                PublicationID = 12,
                MediaID = 1,
                MediaTypeID = 2,
                S3UrlMedia = "http:///bucket-artmais.s3.amazonaws.com/portfolio-contents/3/S3UrlMedia1.jpg",
                Description = "Description1",
                PublicationDate = new DateTime()
            };
            mockPortfolioService.Setup(p => p.GetPublicationByIdToDelete(It.IsAny<int>(), It.IsAny<long>())).Returns(portfolioContent);
            mockS3Client.Setup(x => x.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new AmazonS3Exception(string.Empty));
            var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object, mockS3Client.Object, mockPortfolioService.Object);
            #endregion

            Func<Task> result = async () =>
            {
                await awsService.DeletingAnObjectAsync(new DeleteObjectCommand(113, 12));
            };
            await result.Should().ThrowAsync<InvalidOperationException>().WithMessage("Error to connect to AWS Service");
        }

        [Fact(DisplayName = "DeletingAnObjectAsync should be throw Argument Null Exception")]
        public async Task DeletingAnObjectAsyncShouldBeThrowArgumentNullException()
        {
            #region Mocks
            var mockUserRepository = new Mock<IUserRepository>();
            var mockPortfolioService = new Mock<IPortfolioService>();
            var mockMapper = new Mock<IMapper>();
            var mockS3Client = new Mock<IAmazonS3>();

            mockPortfolioService.Setup(p => p.GetPublicationByIdToDelete(It.IsAny<int>(), It.IsAny<long>())).Throws<ArgumentNullException>();
            mockS3Client.Setup(x => x.DeleteObjectAsync(It.IsAny<DeleteObjectRequest>(), It.IsAny<CancellationToken>())).ThrowsAsync(new AmazonS3Exception(string.Empty));
            var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object, mockS3Client.Object, mockPortfolioService.Object);
            #endregion

            Func<Task> result = async () =>
            {
                await awsService.DeletingAnObjectAsync(new DeleteObjectCommand(113, 12));
            };
            await result.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}