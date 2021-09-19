using ArtmaisBackend.Core.Aws.Dto;
using System.IO;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Aws.Interface
{
    public interface IAwsService
    {
        AwsDto? UploadObject(FileStream file);
        string GeneratePreSignedURL(double duration);
        string Upload();
    }
}