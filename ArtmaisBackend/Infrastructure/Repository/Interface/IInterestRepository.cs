using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IInterestRepository
    {
        public IEnumerable<Interest> Create(InterestRequest interestRequest, int userId);
    }
}
