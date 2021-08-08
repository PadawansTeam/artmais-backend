using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.SignUp.Dto;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Collections.Generic;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class CategorySubcategoryRepository : ICategorySubcategoryRepository
    {
        public CategorySubcategoryRepository(ArtplusContext context)
        {
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public Subcategory Create(string userCategory, string userSubcategory)
        {
            var category = new Category { UserCategory = userCategory, OtherCategory = true };
            this._context.Category.Add(category);
            this._context.SaveChanges();

            var subcategory = new Subcategory { UserSubcategory = userSubcategory, OtherSubcategory = true, CategoryID = category.CategoryID };
            this._context.Subcategory.Add(subcategory);
            this._context.SaveChanges();

            return subcategory;
        }

        public IEnumerable<CategorySubcategoryDto> GetCategoryAndSubcategory()
        {
            var dtos = new List<CategorySubcategoryDto>();

            var result = (from subcategory in this._context.Subcategory
                          join category in this._context.Category on subcategory.CategoryID equals category.CategoryID
                          where subcategory.OtherSubcategory.Equals(false)
                          && category.OtherCategory.Equals(false)
                          select new { Category = category.UserCategory, Subcategory = subcategory.UserSubcategory }).ToList();

            var groupedResults = result.GroupBy(s => s.Category)
                .Select(s => new CategorySubcategoryDto { Category = s.Key, Subcategory = s.Select(s => s.Subcategory).ToList() });

            foreach (var group in groupedResults)
            {
                dtos.Add(group);
            }

            return dtos;
        }

        public Subcategory GetSubcategoryBySubcategory(string userSubcategory)
        {
            var query = from subcategory in this._context.Subcategory
                        where subcategory.UserSubcategory.Equals(userSubcategory)
                        select new Subcategory
                        {
                            SubcategoryID = subcategory.SubcategoryID,
                            UserSubcategory = subcategory.UserSubcategory,
                            OtherSubcategory = subcategory.OtherSubcategory,
                            CategoryID = subcategory.CategoryID,
                        };

            return query.FirstOrDefault();
        }

        public IEnumerable<SubcategoryDto> GetSubcategory()
        {
            var results = (from subcategory in this._context.Subcategory
                           where subcategory.OtherSubcategory.Equals(false)
                           select new SubcategoryDto
                           {
                               SubcategoryID = subcategory.SubcategoryID,
                               Subcategory = subcategory.UserSubcategory,
                               Category = subcategory.Category.UserCategory
                           }).ToList();

            return results;
        }

        public IEnumerable<SubcategoryDto> GetSubcategoryByInterestAndUserId(long userId)
        {
            var results = (from subcategory in this._context.Subcategory
                           join interest in this._context.Interest on subcategory.SubcategoryID equals interest.SubcategoryID
                           where subcategory.OtherSubcategory.Equals(false)
                           && interest.UserID.Equals(userId)
                           && interest.UserSelected
                           select new SubcategoryDto
                           {
                               SubcategoryID = subcategory.SubcategoryID,
                               Subcategory = subcategory.UserSubcategory,
                               Category = subcategory.Category.UserCategory
                           }).ToList();

            return results;
        }
    }
}
