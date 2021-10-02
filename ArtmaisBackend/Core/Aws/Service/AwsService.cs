using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using System.IO;
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

        public async Task<AwsDto?> UploadObjectAsync(UploadObjectCommand uploadObjectCommand)
        {
            var response = await this.WritingAnObjectAsync(uploadObjectCommand);

            if (uploadObjectCommand.IsProfileContent)
            {
                UpdateProfilePicture(response, uploadObjectCommand.UserId);
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

                var putRequest = new PutObjectRequest
                {
                    BucketName = uploadObjectCommand.BucketName,
                    Key = keyName,
                    InputStream = uploadObjectCommand.File.OpenReadStream(),
                    ContentType = uploadObjectCommand.File.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                };

                await client.PutObjectAsync(putRequest);

                var urlAws = string.Format("http://{0}.s3.amazonaws.com/{1}", uploadObjectCommand.BucketName, keyName);

                return new AwsDto(urlAws);
            }
            catch (AmazonS3Exception e)
            {
                throw new AmazonS3Exception(e.Message);
            }
        }

        private void UpdateProfilePicture(AwsDto awsDto, long userId)
        {
            var userInfo = this._userRepository.GetUserById(userId);
            this._mapper.Map(awsDto, userInfo);
            this._userRepository.Update(userInfo);
        }
    }
}