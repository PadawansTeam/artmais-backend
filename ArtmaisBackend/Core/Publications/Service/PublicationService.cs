using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Interface;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Core.Signatures.Interface;
using ArtmaisBackend.Core.SignIn;
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
        public PublicationService(ISignatureService signatureService, IUserService userService, IUserRepository userRepository, IMediaTypeRepository mediaTypeRepository, IPublicationRepository publicationRepository, ICommentRepository commentRepository, ILikeRepository likeRepository, IOptions<SocialMediaConfiguration> options)
        {
            _userService = userService;
            _userRepository = userRepository;
            _mediaTypeRepository = mediaTypeRepository;
            _publicationRepository = publicationRepository;
            _commentRepository = commentRepository;
            _likeRepository = likeRepository;
            _socialMediaConfiguration = options.Value;
            _signatureService = signatureService;
        }

        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMediaTypeRepository _mediaTypeRepository;
        private readonly IPublicationRepository _publicationRepository;
        private readonly ILikeRepository _likeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly ISignatureService _signatureService;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;

        public bool InsertComment(CommentRequest? commentRequest, long userId)
        {
            if (commentRequest.PublicationID is null || commentRequest.Description is null)
            {
                throw new ArgumentNullException();
            }

            _commentRepository.Create(commentRequest, userId);

            return true;
        }

        public bool DeleteComment(int? commentId, long? userId)
        {
            var comment = _commentRepository.GetCommentById(commentId);
            if (comment is null)
            {
                throw new ArgumentNullException();
            }
            var publicationInfo = _publicationRepository.GetPublicationByIdAndUserId(userId, comment.PublicationID);
            if (publicationInfo is null)
            {
                throw new ArgumentNullException();
            }

            _commentRepository.Delete(comment);
            return true;
        }

        private async Task<PublicationCommentsDto?> GetAllCommentsByPublicationId(int? publicationId)
        {
            if (publicationId is null)
            {
                throw new ArgumentNullException();
            }

            var comments = await _commentRepository.GetAllCommentsDtoByPublicationId(publicationId);
            var commentsAmount = comments.Count();

            var publicationCommentsDto = new PublicationCommentsDto(comments, commentsAmount);

            return publicationCommentsDto;
        }

        private PublicationShareLinkDto? GetPublicationShareLinkByPublicationIdAndUserId(long? userId, int? publicationId)
        {
            if (publicationId is null || userId is null)
            {
                throw new ArgumentNullException();
            }

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
            {
                throw new ArgumentNullException();
            }

            var likeInfo = _likeRepository.GetLikeByPublicationIdAndUserId(publicationId, userId);

            if (likeInfo != null)
            {
                return false;
            }

            await _likeRepository.Create(publicationId, userId);
            return true;
        }

        public bool DeleteLike(int? publicationId, long userId)
        {
            if (publicationId is null)
            {
                throw new ArgumentNullException();
            }

            var likeInfo = _likeRepository.GetLikeByPublicationIdAndUserId(publicationId, userId);

            if (likeInfo is null)
            {
                throw new ArgumentNullException();
            }

            _likeRepository.Delete(likeInfo);

            return true;
        }

        private bool GetIsLikedPublication(int? publicationId, long userId)
        {
            if (publicationId is null)
            {
                throw new ArgumentNullException();
            }

            var isLiked = _likeRepository.GetLikeByPublicationIdAndUserId(publicationId, userId);

            if (isLiked is null)
            {
                return false;
            }

            return true;
        }

        private async Task<int?> GetAllLikesByPublicationId(int? publicationId)
        {
            if (publicationId is null)
            {
                throw new ArgumentNullException();
            }

            var likesAmount = await _likeRepository.GetAllLikesAmountByPublicationId(publicationId);

            return likesAmount;
        }

        public async Task<PublicationDto> GetPublicationByIdAndLoggedUser(int? publicationId, long? publicationOwnerUserId, UserJwtData visitorUser)
        {
            if (publicationId is null || publicationOwnerUserId is null)
            {
                throw new ArgumentNullException();
            }

            var publicationOwnerUser = _userRepository.GetUserById(publicationOwnerUserId);
            if (publicationOwnerUser is null)
            {
                throw new ArgumentNullException();
            }

            var portfolio = _publicationRepository.GetAllPublicationsByUserId(publicationOwnerUserId);
            if (portfolio is null)
            {
                throw new ArgumentNullException();
            }

            var publication = portfolio.Where(p => p.PublicationID == publicationId).FirstOrDefault();
            if (publication is null)
            {
                throw new ArgumentNullException();
            }

            if (String.IsNullOrEmpty(publication.Description) || publication.Description == "null")
            {
                publication.Description = "";
            }

            var userCategory = _userRepository.GetSubcategoryByUserId(publicationOwnerUser.UserID);
            var publicationShareLink = GetPublicationShareLinkByPublicationIdAndUserId(publicationOwnerUser.UserID, publication.PublicationID);
            var isLiked = GetIsLikedPublication(publication.PublicationID, visitorUser.UserID);
            var comments = await GetAllCommentsByPublicationId(publication.PublicationID).ConfigureAwait(false);
            var contactProfile = _userService.GetShareProfile(publicationOwnerUser.UserID);
            var likesAmount = await GetAllLikesByPublicationId(publication.PublicationID).ConfigureAwait(false);
            var mediaType = _mediaTypeRepository.GetMediaTypeById(publication.MediaTypeID);
            var isPremium = await _signatureService.GetSignatureByUserId(publicationOwnerUser.UserID);

            var publicationDto = new PublicationDto
            {
                UserId = publicationOwnerUser?.UserID,
                Name = publicationOwnerUser?.Name,
                Username = publicationOwnerUser?.Username,
                UserPicture = publicationOwnerUser?.UserPicture,
                BackgroundPicture = publicationOwnerUser?.BackgroundPicture,
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
                MediaType = mediaType?.Description,
                Comments = comments.Comments,
                CommentsAmount = comments?.CommentsAmount,
                LikesAmount = likesAmount,
                IsLiked = isLiked,
                IsPremium = isPremium
            };

            return publicationDto;
        }
       
        public async Task<PublicationDto> GetPublicationById(int? publicationId, long? publicationOwnerUserId)
        {
            if (publicationId is null || publicationOwnerUserId is null)
            {
                throw new ArgumentNullException();
            }

            var publicationOwnerUser = _userRepository.GetUserById(publicationOwnerUserId);
            if (publicationOwnerUser is null)
            {
                throw new ArgumentNullException();
            }

            var portfolio = _publicationRepository.GetAllPublicationsByUserId(publicationOwnerUserId);
            if (portfolio is null)
            {
                throw new ArgumentNullException();
            }

            var publication = portfolio.Where(p => p.PublicationID == publicationId).FirstOrDefault();
            if (publication is null)
            {
                throw new ArgumentNullException();
            }

            if (String.IsNullOrEmpty(publication.Description) || publication.Description == "null")
            {
                publication.Description = "";
            }

            var userCategory = _userRepository.GetSubcategoryByUserId(publicationOwnerUser.UserID);
            var publicationShareLink = GetPublicationShareLinkByPublicationIdAndUserId(publicationOwnerUser.UserID, publication.PublicationID);
            var comments = await GetAllCommentsByPublicationId(publication.PublicationID).ConfigureAwait(false);
            var contactProfile = _userService.GetShareProfile(publicationOwnerUser.UserID);
            var likesAmount = await GetAllLikesByPublicationId(publication.PublicationID).ConfigureAwait(false);
            var mediaType = _mediaTypeRepository.GetMediaTypeById(publication.MediaTypeID);
            var isPremium = await _signatureService.GetSignatureByUserId(publicationOwnerUser.UserID);

            var publicationDto = new PublicationDto
            {
                UserId = publicationOwnerUser?.UserID,
                Name = publicationOwnerUser?.Name,
                Username = publicationOwnerUser?.Username,
                UserPicture = publicationOwnerUser?.UserPicture,
                BackgroundPicture = publicationOwnerUser?.BackgroundPicture,
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
                MediaType = mediaType?.Description,
                Comments = comments.Comments,
                CommentsAmount = comments?.CommentsAmount,
                LikesAmount = likesAmount,
                IsPremium = isPremium
            };

            return publicationDto;
        }
    }
}
