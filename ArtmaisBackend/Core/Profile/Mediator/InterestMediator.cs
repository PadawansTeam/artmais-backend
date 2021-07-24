using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Security.Claims;

namespace ArtmaisBackend.Core.Profile.Mediator
{
    public class InterestMediator : IInterestMediator
    {
        public InterestMediator(ICategorySubcategoryRepository categorySubcategoryRepository, IInterestRepository interestRepository, IJwtTokenService jwtToken)
        {
            this._categorySubcategoryRepository = categorySubcategoryRepository;
            this._interestRepository = interestRepository;
            this._jwtToken = jwtToken;
        }

        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IJwtTokenService _jwtToken;

        public InterestDto Index(ClaimsPrincipal userClaims)
        {
            var userJwtData = this._jwtToken.ReadToken(userClaims);

            var dto = new InterestDto
            {
                Interests = this._categorySubcategoryRepository.GetSubcategoryByInterestAndUserId(userJwtData.UserID),
                Subcategories = this._categorySubcategoryRepository.GetSubcategory()
            };

            return dto;
        }

        public dynamic Create(InterestRequest interestRequest, ClaimsPrincipal userClaims)
        {
            var userJwtData = this._jwtToken.ReadToken(userClaims);
            return this._interestRepository.DeleteAllAndCreateAll(interestRequest, userJwtData.UserID);
        }
    }
}
