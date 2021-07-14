using ArtmaisBackend.Core.Profile;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IInterestRepository
    {
        public dynamic DeleteAllAndCreateAll(InterestRequest interestRequest, int userId);
    }
}
