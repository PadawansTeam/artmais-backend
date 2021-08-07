using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IProfileAccessRepository
    {
        ProfileAccess Create(long visitorUserId, long visitedUserId);
    }
}
