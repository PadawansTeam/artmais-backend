using System.Collections.Generic;

namespace ArtmaisBackend.Core.Profile
{
    public class InterestRequest
    {
        public IEnumerable<int> SubcategoryID { get; set; }
        public IEnumerable<int> RecommendedSubcategoryID { get; set; }
    }
}
