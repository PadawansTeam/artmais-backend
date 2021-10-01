﻿using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Service;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Users.Service;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Aws
{
    public class AwsServiceTest
    {
        //private const string bucketName = "bucket-artmais";
        //private const string filePath = "profile-pictures/";
        //private static readonly string objectKey = $"{DateTime.Today.ToString("yyyyMMdd")}{new Random((int)DateTime.Now.Ticks).Next().ToString("D14")}";
        //private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        //public static S3CannedACL fileCannedACL = S3CannedACL.PublicRead;

        //[Fact(DisplayName = "UploadObjectAsync should not be null and update database")]
        //public async Task UploadObjectAsyncShouldNotBeNull()
        //{
        //    #region Mocks
        //    var descriptionRequest = new AwsDto("UserPicture");
        //    var mockUserRepository = new Mock<IUserRepository>();
        //    var mockMapper = new Mock<IMapper>();
        //    var fileMock = new Mock<IFormFile>();
        //    var mockS3Client = new Mock<IAmazonS3>();

        //    var keyName = filePath + 3 + "/" + objectKey + ".png";
        //    var content = "UserPicture";
        //    var fileName = "UserPicture.png";
        //    var fileContent = "image/png";
        //    var ms = new MemoryStream();
        //    var writer = new StreamWriter(ms);
        //    writer.Write(content);
        //    writer.Flush();
        //    ms.Position = 0;
        //    fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        //    fileMock.Setup(_ => _.FileName).Returns(fileName);
        //    fileMock.Setup(_ => _.Length).Returns(ms.Length);
        //    fileMock.Setup(_ => _.ContentType).Returns(fileContent);
        //    var file = fileMock.Object;

        //    var userInfo = new User
        //    {
        //        UserID = 3,
        //        UserPicture = "oldUserPicture",
        //    };
        //    var updatedUser = new User
        //    {
        //        UserID = 3,
        //        UserPicture = "newUserPicture",
        //    };
        //    var putRequest = new PutObjectRequest
        //    {
        //        BucketName = bucketName,
        //        Key = keyName,
        //        InputStream = ms,
        //        ContentType = file.ContentType,
        //        CannedACL = fileCannedACL
        //    };
        //    var putResponse = new PutObjectResponse { };

        //    mockUserRepository.Setup(x => x.GetUserById((It.IsAny<int>()))).Returns(userInfo);
        //    mockMapper.Setup(m => m.Map(descriptionRequest, userInfo)).Returns(updatedUser);
        //    mockUserRepository.Setup(x => x.Update((It.IsAny<User>()))).Returns(updatedUser);
        //    mockS3Client.Setup(x => x.PutObjectAsync(putRequest)).ReturnsAsync(putResponse);
        //    #endregion

        //    var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object);

        //    var result = await awsService.WritingAnObjectAsync(file, 3);

        //    result.Should().NotBeNull();
        //}

        //[Fact(DisplayName = "UpdateUserDescription should be false when request is null or empty")]
        //public async Task UpdateUserDescriptionShouldBeFalse()
        //{
        //    var mockUserRepository = new Mock<IUserRepository>();
        //    var mockMapper = new Mock<IMapper>();
        //    var awsService = new AwsService(mockMapper.Object, mockUserRepository.Object);
        //    var fileMock = new Mock<IFormFile>();
        //    var file = fileMock.Object;

        //    Func<Task> result = async () => { await awsService.WritingAnObjectAsync(file, 3); };
        //    await result.Should().ThrowAsync<ArgumentNullException>();
        //    result.Should().BeNull();
        //}
    }
}
