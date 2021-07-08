using artmais_backend.Core.Entities;
using artmais_backend.Core.SignUp;
using System.Collections.Generic;

namespace artmais_backend.Infrastructure.Repository.Interface
{
    public interface ICategorySubcategoryRepository
    {
        public Subcategory Create(string userCategory, string userSubcategory);
        public IEnumerable<CategorySubcategoryDto> GetCategoryAndSubcategory();
        public Subcategory GetSubcategoryBySubcategory(string userSubcategory);
    }
}
