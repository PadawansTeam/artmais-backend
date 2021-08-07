using ArtmaisBackend.Core.Profile.Dto;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IAsyncProfileAccessRepository
    {
        IEnumerable<CategoryRating> GetAllCategoryRating();
    }
}
