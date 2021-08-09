using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Request;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IMediaRepository
    {
        List<Media> GetAllMediasByUserId(long? userId);
        Media Create(PortfolioRequest portfolioRequest, long userId, MediaType mediaTypeContent);
        Media Update(Media media);
    }
}
