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
        private static readonly IAmazonS3 client = new AmazonS3Client(RegionEndpoint.USEast1);
        private readonly IMapper _mapper;

        public async Task<AwsDto?> UploadObjectAsync(IFormFile? file, long userId)
        {       
            var response = await this.WritingAnObjectAsync(file, userId);
            return new AwsDto(response);
        }

        public async Task<string> WritingAnObjectAsync(IFormFile? file, long userId)
        {
            if (file is null) throw new ArgumentNullException();

            try
            {
                var fs = file.OpenReadStream();
                var extension = file.FileName.Split(".");
                var keyName = $"{AwsConfiguration.FilePath}{userId}/{AwsConfiguration.ObjectKey}.{extension[1]}";

                var putRequest = new PutObjectRequest
                {
                    BucketName = AwsConfiguration.BucketName,
                    Key = keyName,
                    InputStream = fs,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                await client.PutObjectAsync(putRequest);

                var urlAws = string.Format("http://{0}.s3.amazonaws.com/{1}", AwsConfiguration.BucketName, keyName);
                var dto = new AwsDto(urlAws);
               
                var userInfo = this._userRepository.GetUserById(userId);
                this._mapper.Map(dto, userInfo);
                this._userRepository.Update(userInfo);

                return urlAws;
            }
            catch (AmazonS3Exception e)
            {
                throw new AmazonS3Exception(e.Message);
            }
        }
    }
}