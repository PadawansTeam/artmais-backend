using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Core.SignIn;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Publications.Interface
{
    public interface IPublicationService
    {
        bool InsertComment(CommentRequest? commentRequest, long userId);

        Task<bool> InsertLike(int? publicationId, long userId);

        bool DeleteLike(int? publicationId, long userId);

        Task<PublicationDto> GetPublicationByIdAndLoggedUser(int? publicationId, long? publicationOwnerUserId, UserJwtData visitorUser);

        Task<PublicationDto> GetPublicationById(int? publicationId, long? publicationOwnerUserId);
    }
}
