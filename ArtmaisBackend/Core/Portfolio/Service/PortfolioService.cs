using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util.File;
using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Portfolio.Service
{
    public class PortfolioService : IPortfolioService
    {
        public PortfolioService(IMediaRepository mediaRepository, IMediaTypeRepository mediaTypeRepository, IPublicationRepository publicationRepository, ILikeRepository likeRepository, ICommentRepository commentRepository, IMapper mapper)
        {
            _mediaRepository = mediaRepository;
            _mediaTypeRepository = mediaTypeRepository;
            _publicationRepository = publicationRepository;
            _likeRepository = likeRepository;
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        private readonly IMediaRepository _mediaRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public PortfolioContentListDto GetLoggedUserPortfolioById(long? userId)
        {
            if (userId is null)
            {
                throw new ArgumentNullException();
            }

            var publicationContent = _publicationRepository.GetAllPublicationsByUserId(userId);

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
            {
                throw new ArgumentNullException();
            }

            var publicationContent = _publicationRepository.GetAllPublicationsByUserId(userId);

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
            {
                throw new ArgumentNullException();
            }

            var mediaTypeContent = _mediaTypeRepository.GetMediaTypeById(mediaTypeId);
            if (mediaTypeContent is null)
            {
                throw new ArgumentNullException();
            }

            var mediaContent = _mediaRepository.Create(portfolioRequest, userId, mediaTypeContent);
            if (mediaContent is null)
            {
                throw new ArgumentNullException();
            }

            var publicationContent = _publicationRepository.Create(portfolioRequest, userId, mediaContent);
            if (publicationContent is null)
            {
                throw new ArgumentNullException();
            }

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
            {
                throw new ArgumentNullException();
            }

            var portfolioInfo = _publicationRepository.GetPublicationByIdAndUserId(userId, portfolioDescriptionRequest.PublicationId);
            if (portfolioInfo is null)
            {
                throw new ArgumentNullException();
            }

            _mapper.Map(portfolioDescriptionRequest, portfolioInfo);
            _publicationRepository.Update(portfolioInfo);

            return true;
        }

        public PortfolioContentDto GetPublicationByIdToDelete(int? publicationId, long userId)
        {
            if (publicationId == null)
            {
                throw new ArgumentNullException();
            }

            var portfolio = _publicationRepository.GetAllPublicationsByUserId(userId);

            if (portfolio == null)
            {
                throw new ArgumentNullException();
            }

            var publication = portfolio.Where(p => p.PublicationID == publicationId).FirstOrDefault();

            return publication;
        }

        public async Task DeleteAllLikes(PortfolioContentDto? portfolioContentDto)
        {
            var likesInfo = await _likeRepository.GetAllLikesByPublicationId(portfolioContentDto.MediaID);

            if (likesInfo is null)
            {
                throw new ArgumentNullException();
            }

            foreach (var likeInfo in likesInfo)
            {
                _mapper.Map(portfolioContentDto, likeInfo);
                _likeRepository.Delete(likeInfo);
            }
        }
        public async Task DeleteAllComments(PortfolioContentDto? portfolioContentDto)
        {
            var commentsInfo = await _commentRepository.GetAllCommentsByPublicationId(portfolioContentDto.MediaID);

            if (commentsInfo is null)
            {
                throw new ArgumentNullException();
            }

            foreach (var commentInfo in commentsInfo)
            {
                _mapper.Map(portfolioContentDto, commentInfo);
                _commentRepository.Delete(commentInfo);
            }
        }

        public void DeletePublication(PortfolioContentDto? portfolioContentDto, long userId)
        {
            var portfolioInfo = _publicationRepository.GetPublicationByIdAndUserId(userId, portfolioContentDto.PublicationID);

            if (portfolioInfo is null)
            {
                throw new ArgumentNullException();
            }

            _mapper.Map(portfolioContentDto, portfolioInfo);

            _publicationRepository.Delete(portfolioInfo);
        }

        public void DeleteMedia(PortfolioContentDto? portfolioContentDto, long userId)
        {
            var mediaInfo = _mediaRepository.GetMediaByIdAndUserId(userId, portfolioContentDto.MediaID);

            if (mediaInfo is null)
            {
                throw new ArgumentNullException();
            }

            _mapper.Map(portfolioContentDto, mediaInfo);

            _mediaRepository.Delete(mediaInfo);
        }

    }
}
