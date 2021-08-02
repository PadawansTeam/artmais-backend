using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IProfileAcessRepository
    {
        ProfileAcess Create(long visitorUserId, long visitedUserId);
    }
}
