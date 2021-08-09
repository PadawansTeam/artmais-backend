using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IMediaTypeRepository
    {
        MediaType? GetMediaTypeById(int? mediaTypeId);
        MediaType Create(string mediaType);
        MediaType Update(MediaType mediaType);
    }
}