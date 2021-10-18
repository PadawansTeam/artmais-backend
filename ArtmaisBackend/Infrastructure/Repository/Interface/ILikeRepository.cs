using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface ILikeRepository
    {
        Task Create(int? publicationId, long userId);

        void Delete(Like like);

        Like? GetLikeByPublicationIdAndUserId(int? publicationId, long userId);

        Task<int> GetAllLikesByPublicationId(int? publicationId);
    }
}
