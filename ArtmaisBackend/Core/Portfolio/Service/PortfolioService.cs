using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util.File;
using AutoMapper;
using System;
using System.Linq;

namespace ArtmaisBackend.Core.Portfolio.Service
{
    public class PortfolioService : IPortfolioService
    {
        public PortfolioService(IMediaRepository mediaRepository, IMediaTypeRepository mediaTypeRepository, IPublicationRepository publicationRepository, IMapper mapper)
        {
            this._mediaRepository = mediaRepository;
            this._mediaTypeRepository = mediaTypeRepository;
            this._publicationRepository = publicationRepository;
            this._mapper = mapper;
        }

        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly IMapper _mapper;

        public PortfolioContentListDto GetLoggedUserPortfolioById(long? userId)
        {
            if (userId is null)
                throw new ArgumentNullException();

            var publicationContent = this._publicationRepository.GetAllPublicationsByUserId(userId);

            var imageList = publicationContent.Where(x => x.MediaTypeID == (int)MediaType.IMAGE).ToList();
            var videoList = publicationContent.Where(x => x.MediaTypeID == (int)MediaType.VIDEO).ToList();
            var audioList = publicationContent.Where(x => x.MediaTypeID == (int)MediaType.AUDIO).ToList();

            var publicationList = new PortfolioContentListDto
            {
                Image = imageList,
                Video = videoList,
                Audio = audioList
            };


            return publicationList;
        }

        public PortfolioContentListDto GetPortfolioByUserId(long? userId)
        {
            if (userId is null)
                throw new ArgumentNullException();

            var publicationContent = this._publicationRepository.GetAllPublicationsByUserId(userId);

            var imageList = publicationContent.Where(x => x.MediaTypeID == (int)MediaType.IMAGE).ToList();
            var videoList = publicationContent.Where(x => x.MediaTypeID == (int)MediaType.VIDEO).ToList();
            var audioList = publicationContent.Where(x => x.MediaTypeID == (int)MediaType.AUDIO).ToList();

            var publicationList = new PortfolioContentListDto
            {
                Image = imageList,
                Video = videoList,
                Audio = audioList
            };


            return publicationList;
        }

        public PortfolioContentDto? InsertPortfolioContent(PortfolioRequest? portfolioRequest, long userId, int mediaTypeId)
        {
            if (portfolioRequest.PortfolioImageUrl is null)
                throw new ArgumentNullException();

            if (portfolioRequest.Description is null)
                portfolioRequest.Description = string.Empty;

            var mediaTypeContent = this._mediaTypeRepository.GetMediaTypeById(mediaTypeId);
            if (mediaTypeContent is null)
                throw new ArgumentNullException();

            var mediaContent = this._mediaRepository.Create(portfolioRequest, userId, mediaTypeContent);
            if (mediaContent is null)
                throw new ArgumentNullException();

            var publicationContent = this._publicationRepository.Create(portfolioRequest, userId, mediaContent);
            if (publicationContent is null)
                throw new ArgumentNullException();

            var portfolioContentDto = new PortfolioContentDto
            {
                UserID = userId,
                PublicationID = publicationContent.PublicationID,
                MediaID = mediaContent.MediaID,
                MediaTypeID = mediaTypeContent.MediaTypeId,
                S3UrlMedia = mediaContent.S3UrlMedia,
                Description = publicationContent?.Description,
                PublicationDate = publicationContent.PublicationDate
            };

            return portfolioContentDto;
        }

        public bool UpdateDescription(PortfolioDescriptionRequest? portfolioDescriptionRequest, long userId)
        {
            if (portfolioDescriptionRequest.PublicationId is null || portfolioDescriptionRequest.PublicationDescription is null)
                throw new ArgumentNullException();

            var portfolioInfo = this._publicationRepository.GetPublicationByIdAndUserId(userId, portfolioDescriptionRequest.PublicationId);
            if (portfolioInfo is null)
                throw new ArgumentNullException();

            this._mapper.Map(portfolioDescriptionRequest, portfolioInfo);
            this._publicationRepository.Update(portfolioInfo);

            return true;
        }

        public PortfolioContentDto GetPortfolioContentById(int? portfolioId, long userId)
        {
            if (portfolioId == null)
                throw new ArgumentNullException();

            var portfolio = this._publicationRepository.GetAllPublicationsByUserId(userId);

            if (portfolio == null)
                throw new ArgumentNullException();

            var portfolioContentDto = portfolio.Where(p => p.PublicationID == portfolioId).FirstOrDefault();

            return portfolioContentDto;
        }

        public bool DeletePublication(PortfolioContentDto? portfolioContentDto, long userId)
        {
            var portfolioInfo = this._publicationRepository.GetPublicationByIdAndUserId(userId, portfolioContentDto.PublicationID);

            if (portfolioInfo is null)
                throw new ArgumentNullException();

            this._mapper.Map(portfolioContentDto, portfolioInfo);

            this._publicationRepository.Delete(portfolioInfo);

            return true;
        }
    }
}
