using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;

namespace ArtmaisBackend.Core.Portfolio.Interface
{
    public interface IPortfolioService
    {
        PortfolioContentListDto GetLoggedUserPortfolioById(long? userId);

        PortfolioContentListDto GetPortfolioByUserId(long? userId);

        PortfolioContentDto? InsertPortfolioContent(PortfolioRequest? portfolioRequest, long userId, int mediaTypeId);

        PortfolioContentDto GetPublicationById(int? publicationId, long userId);

        bool UpdateDescription(PortfolioDescriptionRequest? portfolioDescriptionRequest, long userId);

        void DeletePublication(PortfolioContentDto? portfolioContentDto, long userId);

        void DeleteMedia(PortfolioContentDto? portfolioContentDto, long userId);
    }
}
