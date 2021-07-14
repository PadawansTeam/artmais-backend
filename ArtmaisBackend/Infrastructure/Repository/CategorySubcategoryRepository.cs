﻿using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.SignUp;
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
            _context = context;
        }

        private readonly ArtplusContext _context;

        public Subcategory Create(string userCategory, string userSubcategory)
        {
            var category = new Category { UserCategory = userCategory, OtherCategory = 1 };
            _context.Category.Add(category);
            _context.SaveChanges();

            var subcategory = new Subcategory { UserSubcategory = userSubcategory, OtherSubcategory = 1, CategoryID = category.CategoryID };
            _context.Subcategory.Add(subcategory);
            _context.SaveChanges();

            return subcategory;
        }

        public IEnumerable<CategorySubcategoryDto> GetCategoryAndSubcategory()
        {
            var dtos = new List<CategorySubcategoryDto>();

            var result = (from subcategory in _context.Subcategory
                          join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                          where subcategory.OtherSubcategory.Equals(0)
                          && category.OtherCategory.Equals(0)
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
            var query = from subcategory in _context.Subcategory
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
            var results = (from subcategory in _context.Subcategory
                           where subcategory.OtherSubcategory.Equals(0)
                           select new SubcategoryDto
                           {
                               SubcategoryID = subcategory.SubcategoryID,
                               Subcategory = subcategory.UserSubcategory,
                           }).ToList();

            return results;
        }

        public IEnumerable<SubcategoryDto> GetSubcategoryByInterestAndUserId(int userId)
        {
            var results = (from subcategory in _context.Subcategory
                           join interest in _context.Interest on subcategory.SubcategoryID equals interest.SubcategoryID
                           where subcategory.OtherSubcategory.Equals(0)
                           && interest.UserID.Equals(userId)
                           select new SubcategoryDto
                           {
                               SubcategoryID = subcategory.SubcategoryID,
                               Subcategory = subcategory.UserSubcategory,
                           }).ToList();

            return results;
        }
    }
}
