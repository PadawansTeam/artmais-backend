using ArtmaisBackend.Core.Profile;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IInterestRepository
    {
        dynamic DeleteAllAndCreateAll(InterestRequest interestRequest, long userId);
    }
}
