using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class MediaTypeRepository : IMediaTypeRepository
    {
        public MediaTypeRepository(ArtplusContext context)
        {
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public MediaType? GetMediaTypeById(int? mediaTypeId)
        {
            return this._context.MediaType.FirstOrDefault(mediaType => mediaType.MediaTypeId == mediaTypeId);
        }

        public MediaType Create(string mediaType)
        {
            var mediaTypeContent = new MediaType
            {
                Description = mediaType
            };

            this._context.MediaType.Add(mediaTypeContent);
            this._context.SaveChanges();

            return mediaTypeContent;
        }

        public MediaType Update(MediaType mediaType)
        {
            this._context.MediaType.Update(mediaType);
            this._context.SaveChanges();

            return mediaType;
        }
    }
}
