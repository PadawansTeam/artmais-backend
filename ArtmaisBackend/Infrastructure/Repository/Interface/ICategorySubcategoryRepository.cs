using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.SignUp.Dto;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface ICategorySubcategoryRepository
    {
        Subcategory Create(string userCategory, string userSubcategory);
        IEnumerable<CategorySubcategoryDto> GetCategoryAndSubcategory();
        Subcategory GetSubcategoryBySubcategory(string userSubcategory);
        IEnumerable<SubcategoryDto> GetSubcategory();
        IEnumerable<SubcategoryDto> GetSubcategoryByInterestAndUserId(long userId);
    }
}
