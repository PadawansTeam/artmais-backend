using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.Profile
{
    public class Interest : IInterest
    {
        public Interest(ICategorySubcategoryRepository categorySubcategoryRepository, IJwtToken jwtToken)
        {
            _categorySubcategoryRepository = categorySubcategoryRepository;
            _jwtToken = jwtToken;
        }

        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IJwtToken _jwtToken;

        public IEnumerable<SubcategoryDto> Index()
        {
            return _categorySubcategoryRepository.GetSubcategory();
        }
    }
}
