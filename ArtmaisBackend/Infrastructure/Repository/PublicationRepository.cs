using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class PublicationRepository : IPublicationRepository
    {
        public PublicationRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public Publication? GetPublicationByIdAndUserId(long? userId, int? publicationId)
        {
            return _context.Publication.FirstOrDefault(publication => publication.UserID == userId && publication.PublicationID == publicationId);
        }

        public List<PortfolioContentDto> GetAllPublicationsByUserId(long? userId)
        {
            var query = (from publication in _context.Publication
                         join media in _context.Media on publication.MediaID equals media.MediaID
                         where publication.UserID.Equals(userId)
                         select new PortfolioContentDto
                         {
                             UserID = userId,
                             PublicationID = publication.PublicationID,
                             MediaID = publication.MediaID,
                             MediaTypeID = media.MediaTypeID,
                             S3UrlMedia = publication.Media.S3UrlMedia,
                             Description = publication.Description,
                             PublicationDate = publication.PublicationDate
                         }).ToList();

            return query;
        }

        public Publication Create(PortfolioRequest portfolioRequest, long userId, Media media)
        {
            var publicationContent = new Publication
            {
                UserID = userId,
                MediaID = media.MediaID,
                Description = portfolioRequest.Description,
                PublicationDate = DateTime.UtcNow
            };

            _context.Publication.Add(publicationContent);
            _context.SaveChanges();

            return publicationContent;
        }

        public Publication Update(Publication publication)
        {
            _context.Publication.Update(publication);
            _context.SaveChanges();

            return publication;
        }

        public void Delete(Publication publication)
        {
            _context.Publication.Remove(publication);
            _context.SaveChanges();
        }
    }
}
