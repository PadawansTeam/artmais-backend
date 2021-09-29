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
        public AwsService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        private const string bucketName = "bucket-artmais";
        private const string filePath = "profile-pictures/";
        private static readonly string objectKey = $"{DateTime.Today.ToString("yyyyMMdd")}{new Random((int)DateTime.Now.Ticks).Next().ToString("D14")}";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        public static S3CannedACL fileCannedACL = S3CannedACL.PublicRead;
        private static IAmazonS3 client;
        private readonly IMapper _mapper;

        public async Task<AwsDto?> UploadObjectAsync(IFormFile? file, long userId)
        {
            client = new AmazonS3Client(bucketRegion);
            var response = await WritingAnObjectAsync(file, userId);
            return new AwsDto
            {
                Picture = response
            };
        }
        public async Task<string> WritingAnObjectAsync(IFormFile? file, long userId)
        {
            try
            {
                var fs = file.OpenReadStream();
                var extension = file.FileName.Split(".");
                var keyName = filePath + userId + "/" + objectKey + "." + extension[1];
                var putRequest1 = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName,
                    InputStream = fs,
                    ContentType = file.ContentType,
                    CannedACL = fileCannedACL
                };

                PutObjectResponse response = await client.PutObjectAsync(putRequest1);
                return string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, keyName); ;
            }
            catch (AmazonS3Exception)
            {
                var error = "Error encountered ***. Message:'{0}' when writing an object";
                return error;
            }
            catch (Exception)
            {
                var error = "Unknown encountered on server. Message:'{0}' when writing an object";
                return error;
            }
        }
    }
}