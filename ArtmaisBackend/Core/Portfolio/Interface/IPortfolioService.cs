using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.Portfolio.Interface
{
    public interface IPortfolioService
    {
        List<PortfolioContentDto> GetLoggedUserPortfolioById(long? userId);

        List<PortfolioContentDto> GetPortfolioByUserId(long? userId);

        PortfolioContentDto? InsertPortfolioContent(PortfolioRequest? portfolioRequest, long userId, int mediaTypeId);

        bool UpdateDescription(PortfolioDescriptionRequest? descriptionRequest, long userId);
    }
}
