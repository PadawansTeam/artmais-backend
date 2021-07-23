using System.Collections.Generic;

namespace ArtmaisBackend.Core.SignUp.Dto
{
    public class CategorySubcategoryDto
    {
        public string Category { get; set; }
        public IEnumerable<string> Subcategory { get; set; }
    }
}
