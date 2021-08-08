using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class PublicationRepository : IPublicationRepository
    {
        public PublicationRepository(ArtplusContext context)
        {
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public Publication? GetPublicationByIdAndUserId(long? userId, int? publicationId)
        {
            return this._context.Publication.FirstOrDefault(publication => publication.UserID == userId && publication.PublicationID == publicationId);
        }

        public List<PortfolioContentDto> GetAllPublicationsByUserId(long? userId)
        {
            return this._context.Publication
                .Include(x => x.Media)
                .Where(publication => publication.UserID == userId)
                .Select(publication => new PortfolioContentDto
                {
                    UserId = userId,
                    PublicationId = publication.PublicationID,
                    MediaId = publication.MediaID,
                    S3UrlMedia = publication.Media.S3UrlMedia,
                    Description = publication.Description,
                    PublicationDate = publication.PublicationDate
                }).ToList();
        }

        public Publication Create(PortfolioRequest portfolioRequest, long userId, Media media)
        {
            var publicationContent = new Publication
            {
                UserID = userId,
                MediaID = media.MediaID,
                Description = portfolioRequest.Description,
                PublicationDate = DateTime.Now
            };

            this._context.Publication.Add(publicationContent);
            this._context.SaveChanges();

            return publicationContent;
        }

        public Publication Update(Publication publication)
        {
            this._context.Publication.Update(publication);
            this._context.SaveChanges();

            return publication;
        }
    }
}
