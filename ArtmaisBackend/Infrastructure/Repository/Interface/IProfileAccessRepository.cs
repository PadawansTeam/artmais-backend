using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IProfileAccessRepository
    {
        ProfileAcess Create(long visitorUserId, long visitedUserId);
    }
}
