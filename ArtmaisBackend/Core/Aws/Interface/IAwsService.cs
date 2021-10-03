using ArtmaisBackend.Core.Aws.Dto;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Aws.Interface
{
    public interface IAwsService
    {
        Task<AwsDto?> UploadObjectAsync(UploadObjectCommand uploadObjectCommand);
        Task<AwsDto> WritingAnObjectAsync(UploadObjectCommand uploadObjectCommand);
    }
}