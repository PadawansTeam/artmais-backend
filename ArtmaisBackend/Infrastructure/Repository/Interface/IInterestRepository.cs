using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IInterestRepository
    {
        Task<IEnumerable<Interest>> DeleteAllAndCreateAllAsync(InterestRequest interestRequest, long userId);
    }
}
