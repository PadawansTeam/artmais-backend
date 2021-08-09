using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class MediaRepository : IMediaRepository
    {
        public MediaRepository(ArtplusContext context)
        {
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public List<Media> GetAllMediasByUserId(long? userId)
        {
            return this._context.Media.Where(media => media.UserID == userId).ToList();
        }

        public Media Create(PortfolioRequest portfolioRequest, long userId, MediaType mediaTypeContent)
        {
            var mediaContent = new Media
            {
                UserID = userId,
                MediaTypeID = mediaTypeContent.MediaTypeId,
                S3UrlMedia = portfolioRequest.PortfolioImageUrl
            };

            this._context.Media.Add(mediaContent);
            this._context.SaveChanges();

            return mediaContent;
        }

        public Media Update(Media media)
        {
            this._context.Media.Update(media);
            this._context.SaveChanges();

            return media;
        }
    }
}
