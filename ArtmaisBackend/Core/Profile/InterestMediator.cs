using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Collections.Generic;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile
{
    public class InterestMediator : IInterestMediator
    {
        public InterestMediator(ICategorySubcategoryRepository categorySubcategoryRepository, IInterestRepository interestRepository, IJwtToken jwtToken)
        {
            _categorySubcategoryRepository = categorySubcategoryRepository;
            _interestRepository = interestRepository;
            _jwtToken = jwtToken;
        }

        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IJwtToken _jwtToken;

        public IEnumerable<SubcategoryDto> Index()
        {
            return _categorySubcategoryRepository.GetSubcategory();
        }

        public IEnumerable<Interest> Create(InterestRequest interestRequest, ClaimsPrincipal userClaims)
        {
            var userJwtData = _jwtToken.ReadToken(userClaims);
            return _interestRepository.Create(interestRequest, userJwtData.UserID);
        }
    }
}
