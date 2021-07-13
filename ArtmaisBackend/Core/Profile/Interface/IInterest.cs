using System.Collections.Generic;

namespace ArtmaisBackend.Core.Profile.Interface
{
    public interface IInterest
    {
        public IEnumerable<SubcategoryDto> Index();
    }
}
