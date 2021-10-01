using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Aws.Service
{
    public class AwsService : IAwsService
    {
        public AwsService(IMapper mapper, IUserRepository userRepository)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
        }

        private readonly IUserRepository _userRepository;
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
            var response = await this.WritingAnObjectAsync(file, userId);
            return new AwsDto(response);
        }

        public async Task<string> WritingAnObjectAsync(IFormFile? file, long userId)
        {
            try
            {
                if (file is null)
                    throw new ArgumentNullException();

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

                var urlAws = string.Format("http://{0}.s3.amazonaws.com/{1}", bucketName, keyName);
                var request = new AwsDto(urlAws);
                var userInfo = this._userRepository.GetUserById(userId);
                var updateUserPicture = this._userRepository.UpdateUserPicture(urlAws);
                this._mapper.Map(request, userInfo);
                this._userRepository.Update(userInfo);

                return urlAws;
            }
            catch (AmazonS3Exception e)
            {
                throw new AmazonS3Exception(e.Message);
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}