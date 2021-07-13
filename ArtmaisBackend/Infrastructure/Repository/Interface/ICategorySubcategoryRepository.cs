using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.SignUp;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface ICategorySubcategoryRepository
    {
        public Subcategory Create(string userCategory, string userSubcategory);
        public IEnumerable<CategorySubcategoryDto> GetCategoryAndSubcategory();
        public Subcategory GetSubcategoryBySubcategory(string userSubcategory);
        public IEnumerable<SubcategoryDto> GetSubcategory();
    }
}
