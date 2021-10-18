using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Interface;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Infrastructure;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Publications.Service
{
    public class PublicationService : IPublicationService
    {
        public PublicationService(IUserService userService, IUserRepository userRepository, IPublicationRepository publicationRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, IOptions<SocialMediaConfiguration> options)
        {
            _userService = userService;
            _userRepository = userRepository;
            _publicationRepository = publicationRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _socialMediaConfiguration = options.Value;
        }

        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;

        public bool InsertComment(CommentRequest? commentRequest, long userId)
        {
            if (commentRequest.PublicationID is null || commentRequest.Description is null)
                throw new ArgumentNullException();

            _commentRepository.Create(commentRequest, userId);

            return true;
        }

        private async Task<PublicationCommentsDto?> GetAllCommentsByPublicationId(int? publicationId)
        {
            if (publicationId is null)
                throw new ArgumentNullException();

            var comments = await _commentRepository.GetAllCommentsByPublicationId(publicationId);
            var commentsAmount = comments.Count();

            var publicationCommentsDto = new PublicationCommentsDto(comments, commentsAmount);

            return publicationCommentsDto;
        }

        private PublicationShareLinkDto? GetPublicationShareLinkByPublicationIdAndUserId(long? userId, int? publicationId)
        {
            if (publicationId is null || userId is null)
                throw new ArgumentNullException();

            var publicationShareLinkDto = new PublicationShareLinkDto
            {
                Facebook = $"{_socialMediaConfiguration.Facebook}{_socialMediaConfiguration.ArtMais}{userId}/publicacao/{publicationId}{ShareLinkMessages.MessageShareComment}",
                Twitter = $"{_socialMediaConfiguration.Twitter}{_socialMediaConfiguration.ArtMais}{userId}/publicacao/{publicationId}{ShareLinkMessages.MessageShareComment}",
                Whatsapp = $"{_socialMediaConfiguration.Whatsapp}?text={_socialMediaConfiguration.ArtMais}{userId}/publicacao/{publicationId}{ShareLinkMessages.MessageShareComment}"
            };
            return publicationShareLinkDto;
        }

        public async Task<bool> InsertLike(int? publicationId, long userId)
        {
            if (publicationId is null)
                throw new ArgumentNullException();

            await _likeRepository.Create(publicationId, userId);

            return true;
        }

        public bool DeleteLike(int? publicationId, long userId)
        {
            if (publicationId is null)
                throw new ArgumentNullException();

            var likeInfo = _likeRepository.GetLikeByPublicationIdAndUserId(publicationId, userId);

            if (likeInfo is null)
                throw new ArgumentNullException();

            _likeRepository.Delete(likeInfo);

            return true;
        }

        private bool GetIsLikedPublication(int? publicationId, long userId)
        {
            if (publicationId is null)
                throw new ArgumentNullException();

            var isLiked = _likeRepository.GetLikeByPublicationIdAndUserId(publicationId, userId);

            if (isLiked is null)
                return false;

            return true;
        }

        private async Task<int?> GetAllLikesByPublicationId(int? publicationId)
        {
            if (publicationId is null)
                throw new ArgumentNullException();

            var likesAmount = await _likeRepository.GetAllLikesByPublicationId(publicationId);

            return likesAmount;
        }

        public async Task<PublicationDto> GetPublicationById(int? publicationId, long? userId)
        {
            if (publicationId is null || userId is null)
                throw new ArgumentNullException();

            var user = _userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();


            var portfolio = _publicationRepository.GetAllPublicationsByUserId(userId);
            if (portfolio is null)
                throw new ArgumentNullException();

            var publication = portfolio.Where(p => p.PublicationID == publicationId).FirstOrDefault();
            if (publication is null)
                throw new ArgumentNullException();

            var userCategory = _userRepository.GetSubcategoryByUserId(user.UserID);
            var publicationShareLink = GetPublicationShareLinkByPublicationIdAndUserId(user.UserID, publication.PublicationID);
            var isLiked = GetIsLikedPublication(publication.PublicationID, user.UserID);
            var comments = await GetAllCommentsByPublicationId(publication.PublicationID);
            var contactProfile = _userService.GetShareProfile(user.UserID);
            var likesAmount = await GetAllLikesByPublicationId(publication.PublicationID);

            var publicationDto = new PublicationDto
            {
                Name = user?.Name,
                Username = user?.Username,
                UserPicture = user?.UserPicture,
                BackgroundPicture = user?.BackgroundPicture,
                Category = userCategory?.Category,
                Subcategory = userCategory?.Subcategory,
                UserFacebook = contactProfile?.Facebook,
                UserInstagram = contactProfile?.Instagram,
                UserTwitter = contactProfile?.Twitter,
                PublicationFacebook = publicationShareLink?.Facebook,
                PublicationTwitter = publicationShareLink?.Twitter,
                PublicationWhatsapp = publicationShareLink?.Whatsapp,
                S3UrlMedia = publication?.S3UrlMedia,
                Description = publication?.Description,
                PublicationDate = publication?.PublicationDate,
                Comments = comments?.Comments,
                CommentsAmount = comments?.CommentsAmount,
                LikesAmount = likesAmount,
                IsLiked = isLiked
            };

            return publicationDto;
        }
    }
}
