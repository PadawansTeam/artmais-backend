using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
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

        public InterestDto Index(ClaimsPrincipal userClaims)
        {
            var userJwtData = _jwtToken.ReadToken(userClaims);

            var dto = new InterestDto
            {
                Interests = _categorySubcategoryRepository.GetSubcategoryByInterestAndUserId(userJwtData.UserID),
                Subcategories = _categorySubcategoryRepository.GetSubcategory()
            };

            return dto;
        }

        public dynamic Create(InterestRequest interestRequest, ClaimsPrincipal userClaims)
        {
            var userJwtData = _jwtToken.ReadToken(userClaims);
            return _interestRepository.DeleteAllAndCreateAll(interestRequest, userJwtData.UserID);
        }
    }
}
