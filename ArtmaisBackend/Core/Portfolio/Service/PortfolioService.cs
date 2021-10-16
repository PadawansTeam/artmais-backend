using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Infrastructure;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util.File;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Portfolio.Service
{
    public class PortfolioService : IPortfolioService
    {
        public PortfolioService(IMediaRepository mediaRepository, IMediaTypeRepository mediaTypeRepository, IPublicationRepository publicationRepository, ICommentRepository commentRepository, IOptions<SocialMediaConfiguration> options, IMapper mapper)
        {
            this._mediaRepository = mediaRepository;
            this._mediaTypeRepository = mediaTypeRepository;
            this._publicationRepository = publicationRepository;
            this._commentRepository = commentRepository;
            this._socialMediaConfiguration = options.Value;
            this._mapper = mapper;
        }

        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;
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

        public PortfolioContentDto GetPublicationById(int? publicationId, long userId)
        {
            if (publicationId == null)
                throw new ArgumentNullException();

            var portfolio = this._publicationRepository.GetAllPublicationsByUserId(userId);

            if (portfolio == null)
                throw new ArgumentNullException();

            var publication = portfolio.Where(p => p.PublicationID == publicationId).FirstOrDefault();

            return publication;
        }

        public void DeletePublication(PortfolioContentDto? portfolioContentDto, long userId)
        {
            var portfolioInfo = this._publicationRepository.GetPublicationByIdAndUserId(userId, portfolioContentDto.PublicationID);

            if (portfolioInfo is null)
                throw new ArgumentNullException();

            this._mapper.Map(portfolioContentDto, portfolioInfo);

            this._publicationRepository.Delete(portfolioInfo);
        }

        public void DeleteMedia(PortfolioContentDto? portfolioContentDto, long userId)
        {
            var mediaInfo = this._mediaRepository.GetMediaByIdAndUserId(userId, portfolioContentDto.MediaID);

            if (mediaInfo is null)
                throw new ArgumentNullException();

            this._mapper.Map(portfolioContentDto, mediaInfo);

            this._mediaRepository.Delete(mediaInfo);
        }

        public bool InsertComment(CommentRequest? commentRequest, long userId)
        {
            if (commentRequest.PublicationID is null || commentRequest.Description is null)
                throw new ArgumentNullException();

            this._commentRepository.Create(commentRequest, userId);

            return true;
        }

        public async Task<PublicationCommentsDto?> GetAllCommentsByPublicationId(int? publicationId)
        {
            if (publicationId is null)
                throw new ArgumentNullException();

            var comments = await this._commentRepository.GetAllCommentsByPublicationId(publicationId);
            var commentsAmount = comments.Count();

            var publicationCommentsDto = new PublicationCommentsDto(comments, commentsAmount);

            return publicationCommentsDto;
        }

        public PublicationShareLinkDto? GetPublicationShareLinkByPublicationIdAndUserId(long? userId, int? publicationId)
        {
            if (publicationId is null || userId is null)
                throw new ArgumentNullException();

            var publicationShareLinkDto = new PublicationShareLinkDto
            {
                Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{userId}/publication/{publicationId}{ShareLinkMessages.MessageShareComment}",
                Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{userId}/publication/{publicationId}{ShareLinkMessages.MessageShareComment}",
                Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{userId}/publication/{publicationId}{ShareLinkMessages.MessageShareComment}"
            };
            return publicationShareLinkDto;
        }

    }
}
