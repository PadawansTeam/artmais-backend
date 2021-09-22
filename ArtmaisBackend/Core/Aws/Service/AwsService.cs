using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Aws.Service
{
    public class AwsService : IAwsService
    {
        public AwsService(IMapper mapper, IAmazonS3 amazonS3)
        {
            this._mapper = mapper;
            this._amazonS3 = amazonS3;
        }

        private const string bucketName = "bucket-artmais";
        private static readonly string objectKey = $"ARTMAIS{DateTime.Today.ToString("yyyyMMdd")}{new Random((int)DateTime.Now.Ticks).Next().ToString("D14")}";
        private const string filePath = "profile-pictures/";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
        private readonly IAmazonS3 _amazonS3;
        private readonly IMapper _mapper;

        public async Task<AwsDto?> UploadObjectAsync(IFormFile? file, long userId)
        {
            var putRequest = new PutObjectRequest()
            {
                BucketName = bucketName,
                Key = filePath + userId + "/" + objectKey + ".jpg",
                ContentType = file.ContentType,
                ContentBody = file.ContentDisposition,
                CannedACL = S3CannedACL.PublicRead,
            };

            var result = await this._amazonS3.PutObjectAsync(putRequest);
            
            return new AwsDto
            {
                Picture = result.RequestCharged
            };
        }
    }
}
