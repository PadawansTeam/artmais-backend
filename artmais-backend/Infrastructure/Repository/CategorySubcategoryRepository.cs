using artmais_backend.Core.Entities;
using artmais_backend.Infrastructure.Data;
using artmais_backend.Infrastructure.Repository.Interface;
using System.Linq;

namespace artmais_backend.Infrastructure.Repository
{
    public class CategorySubcategoryRepository : ICategorySubcategoryRepository
    {
        public CategorySubcategoryRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public void Create(string otherCategory, string otherSubcategory)
        {
            var categorySubcategory = new CategorySubcategory
            {
                Category = new Category { UserCategory = otherCategory, OtherCategory = 1 },
                Subcategory = new Subcategory { UserSubcategory = otherSubcategory, OtherSubcategory = 1 }
            };

            _context.CategorySubcategory.Add(categorySubcategory);
            _context.SaveChanges();
        }

        public CategorySubcategory GetCategorySubcategory()
        {
            var query = from categorysubcategory in _context.CategorySubcategory
                        where categorysubcategory.Category.OtherCategory.Equals(0)
                        && categorysubcategory.Subcategory.Equals(0)
                        select new CategorySubcategory
                        {
                            ID = categorysubcategory.ID,
                            CategoryID = categorysubcategory.CategoryID,
                            Category = categorysubcategory.Category,
                            SubcategoryID = categorysubcategory.SubcategoryID,
                            Subcategory = categorysubcategory.Subcategory,
                        };

            return query.FirstOrDefault();
        }
    }
}
