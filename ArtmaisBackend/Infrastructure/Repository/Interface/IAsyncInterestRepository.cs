using ArtmaisBackend.Core.Profile.Dto;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IAsyncInterestRepository
    {
        void Create(CategoryRating categoryRating);
    }
}
