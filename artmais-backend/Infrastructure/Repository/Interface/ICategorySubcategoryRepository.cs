using artmais_backend.Core.Entities;

namespace artmais_backend.Infrastructure.Repository.Interface
{
    public interface ICategorySubcategoryRepository
    {
        public void Create(string otherCategory, string otherSubcategory);
        public CategorySubcategory GetCategorySubcategory();
    }
}
