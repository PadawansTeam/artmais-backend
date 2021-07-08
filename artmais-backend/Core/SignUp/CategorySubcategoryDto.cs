using System.Collections.Generic;

namespace artmais_backend.Core.SignUp
{
    public class CategorySubcategoryDto
    {
        public string Category { get; set; }
        public IEnumerable<string> Subcategory { get; set; }
    }
}
