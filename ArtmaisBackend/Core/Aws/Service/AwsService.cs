using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Aws.Service
{
    public class AwsService : IAwsService
    {
        public AwsService(IAmazonS3 amazonS3)
        {
            _amazonS3Client = new AmazonS3Client(bucketRegion);
        }

        private const string bucketName = "bucket-artmais";
        private static readonly string objectKey = $"ARTMAIS{DateTime.Today.ToString("yyyyMMdd")}{new Random((int)DateTime.Now.Ticks).Next().ToString("D14")}";
        private const string filePath = "profile-pictures/";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private readonly IAmazonS3 _amazonS3Client;

        public async Task<AwsDto?> UploadObjectAsync(IFormFile? file, long userId)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = filePath + userId + "/" + objectKey + ".jpg",
                ContentType = file.ContentType,
                ContentBody = file.ContentDisposition,
                CannedACL = S3CannedACL.PublicRead,
            };

            var result = await _amazonS3Client.PutObjectAsync(putRequest);

            return new AwsDto
            {
                Picture = result.RequestCharged
            };
        }
    }
}
