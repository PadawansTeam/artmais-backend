using Amazon.S3;
using Amazon.S3.Model;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Interface;
using ArtmaisBackend.Core.Aws.Request;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Aws.Service
{
    public class AwsService : IAwsService
    {
        public AwsService(IMapper mapper, IUserRepository userRepository, IAmazonS3 client, IPortfolioService portfolioService)
        {
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._client = client;
            this._portfolioService = portfolioService;
        }

        private readonly IUserRepository _userRepository;
        private readonly IAmazonS3 _client;
        private readonly IMapper _mapper;
        private readonly IPortfolioService _portfolioService;

        public async Task<AwsDto?> UploadObjectAsync(UploadObjectCommand uploadObjectCommand)
        {
            var response = await this.WritingAnObjectAsync(uploadObjectCommand);

            if (uploadObjectCommand.IsProfileContent)
            {
                this.UpdateProfilePicture(response, uploadObjectCommand.UserId);
                return response;
            }

            return response;
        }

        public async Task<AwsDto> WritingAnObjectAsync(UploadObjectCommand uploadObjectCommand)
        {
            try
            {
                var extension = Path.GetExtension(uploadObjectCommand.File.FileName);
                var keyName = $"{uploadObjectCommand.FilePath}/{uploadObjectCommand.UserId}/{uploadObjectCommand.ObjectKey}{extension}";
                using var fileStream = uploadObjectCommand.File.OpenReadStream();

                var putRequest = new PutObjectRequest
                {
                    BucketName = uploadObjectCommand.BucketName,
                    Key = keyName,
                    InputStream = fileStream,
                    ContentType = uploadObjectCommand.File.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                await this._client.PutObjectAsync(putRequest);

                var urlAws = string.Format("http://{0}.s3.amazonaws.com/{1}", uploadObjectCommand.BucketName, keyName);

                return new AwsDto(urlAws);
            }
            catch (AmazonS3Exception e)
            {
                throw new InvalidOperationException("Error to connect to AWS Service", e);
            }
        }

        private void UpdateProfilePicture(AwsDto awsDto, long userId)
        {
            var userInfo = this._userRepository.GetUserById(userId);
            this._mapper.Map(awsDto, userInfo);
            this._userRepository.Update(userInfo);
        }

        public async Task<bool> DeletingAnObjectAsync(DeleteObjectCommand deleteObjectCommand)
        {
            try
            {
                var portfolioContent = this._portfolioService.GetPortfolioContentById(deleteObjectCommand.PortfolioId, deleteObjectCommand.UserId);

                var keyName = portfolioContent.S3UrlMedia;

                var deleteObjectRequest = new DeleteObjectRequest
                {
                    BucketName = deleteObjectCommand.BucketName,
                    Key = keyName
                };

                await this._client.DeleteObjectAsync(deleteObjectRequest);

                this.DeletePortfolioContent(portfolioContent, deleteObjectCommand.UserId);

                return true;
            }
            catch (AmazonS3Exception e)
            {
                throw new InvalidOperationException("Error to connect to AWS Service", e);
            }
        }

        private void DeletePortfolioContent(PortfolioContentDto portfolioContent, long userId)
        {
            this._portfolioService.DeletePublication(portfolioContent, userId);
            this._portfolioService.DeleteMedia(portfolioContent, userId);
        }
    }
}