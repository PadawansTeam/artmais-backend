using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;

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

        public List<PortfolioContentDto> GetLoggedUserPortfolioById(long? userId)
        {
            if (userId is null)
                throw new ArgumentNullException();

            var publicationContent = this._publicationRepository.GetAllPublicationsByUserId(userId);

            return publicationContent;
        }

        public List<PortfolioContentDto> GetPortfolioByUserId(long? userId)
        {
            if (userId is null)
                throw new ArgumentNullException();

            var publicationContent = this._publicationRepository.GetAllPublicationsByUserId(userId);

            return publicationContent;
        }

        public PortfolioContentDto? InsertPortfolioContent(PortfolioRequest? portfolioRequest, long userId, int mediaTypeId)
        {
            if (portfolioRequest.PortfolioImageUrl is null)
                throw new ArgumentNullException();

            if (portfolioRequest.Description is null)
                portfolioRequest.Description = "";

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
    }
}
