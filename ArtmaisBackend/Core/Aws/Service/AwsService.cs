using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Interface;
using AutoMapper;
using System;
using System.IO;
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
        private static readonly string objectKey = $"DEL{DateTime.Today.ToString("yyyyMMdd")}{new Random((int)DateTime.Now.Ticks).Next().ToString("D14")}";
        private const string filePath = "*** provide the full path name of the file to upload ***";
        private const double timeoutDuration = 12;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USWest2;
        private static IAmazonS3 s3Client;
        private readonly IMapper _mapper;

        public async Task<AwsDto?> UploadObject(FileStream file)
        {
            s3Client = new AmazonS3Client(bucketRegion);
            var fileTransferUtility = new TransferUtility(s3Client);
            
            // Aqui ele faz o upload 
            await fileTransferUtility.UploadAsync(file, bucketName, objectKey);
            var urlPicture = Upload();
            return new AwsDto
            {
                Picture = urlPicture
            };
        }

        // Pelo que eu entendi, aqui ele só gera o url assinado
        // Não esta fazendo o upload
        public string GeneratePreSignedURL(double duration)
        {
            string urlString = "";
            try
            {
                GetPreSignedUrlRequest request1 = new GetPreSignedUrlRequest
                {
                    BucketName = bucketName,
                    Key = objectKey,
                    Expires = DateTime.UtcNow.AddHours(duration)
                };
                urlString = s3Client.GetPreSignedURL(request1);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
            return urlString;
        }

        public string Upload()
        {
            s3Client = new AmazonS3Client(bucketRegion);
            string urlString = GeneratePreSignedURL(timeoutDuration);
            return urlString;
        }
    }
}
