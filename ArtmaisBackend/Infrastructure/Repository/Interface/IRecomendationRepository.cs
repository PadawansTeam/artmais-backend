using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IRecomendationRepository
    {
        Task<Recomendation> AddAsync(int interestId, int subcategoryId);
        void DeleteAllByUserId(long userId);
    }
}
