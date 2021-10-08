using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPublicationRepository
    {
        Publication? GetPublicationByIdAndUserId(long? userId, int? publicationId);
        List<PortfolioContentDto> GetAllPublicationsByUserId(long? userId);
        Publication Create(PortfolioRequest portfolioRequest, long userId, Media media);
        Publication Update(Publication publication);
        void Delete(Publication publication);
    }
}
