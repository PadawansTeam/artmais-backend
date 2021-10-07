using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Request;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Aws.Interface
{
    public interface IAwsService
    {
        Task<AwsDto?> UploadObjectAsync(UploadObjectCommand uploadObjectCommand);
        Task<AwsDto> WritingAnObjectAsync(UploadObjectCommand uploadObjectCommand);
        Task<bool> DeletingAnObjectAsync(DeleteObjectCommand deleteObjectCommand);
    }
}