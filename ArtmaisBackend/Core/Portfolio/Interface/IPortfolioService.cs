using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Portfolio.Interface
{
    public interface IPortfolioService
    {
        PortfolioContentListDto GetLoggedUserPortfolioById(long? userId);

        PortfolioContentListDto GetPortfolioByUserId(long? userId);

        PortfolioContentDto? InsertPortfolioContent(PortfolioRequest? portfolioRequest, long userId, int mediaTypeId);

        bool UpdateDescription(PortfolioDescriptionRequest? portfolioDescriptionRequest, long userId);

        PortfolioContentDto GetPublicationByIdToDelete(int? publicationId, long userId);

        Task DeleteAllLikes(PortfolioContentDto? portfolioContentDto);

        Task DeleteAllComments(PortfolioContentDto? portfolioContentDto);

        void DeletePublication(PortfolioContentDto? portfolioContentDto, long userId);

        void DeleteMedia(PortfolioContentDto? portfolioContentDto, long userId);
    }
}
