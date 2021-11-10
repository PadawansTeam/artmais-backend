using System.Collections.Generic;

namespace ArtmaisBackend.Core.Profile.Dto
{
    public class InterestDto
    {
        public IEnumerable<SubcategoryDto> Interests { get; set; }
        public IEnumerable<SubcategoryDto> Subcategories { get; set; }
        public IEnumerable<SubcategoryDto> Recommendations { get; set; }
    }
}
