using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.Profile.Interface;
using ArtmaisBackend.Core.Recomendation.Services;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Profile.Mediator
{
    public class InterestMediator : IInterestMediator
    {
        public InterestMediator(
            ICategorySubcategoryRepository categorySubcategoryRepository,
            IInterestRepository interestRepository,
            IJwtTokenService jwtToken,
            IRecomendationService recommendationService,
            IRecommendationRepository recommendationRepository,
            ILogger<InterestMediator> logger)
        {
            _categorySubcategoryRepository = categorySubcategoryRepository ?? throw new ArgumentNullException(nameof(categorySubcategoryRepository));
            _interestRepository = interestRepository ?? throw new ArgumentNullException(nameof(interestRepository));
            _jwtToken = jwtToken ?? throw new ArgumentNullException(nameof(jwtToken));
            _recommendationService = recommendationService ?? throw new ArgumentNullException(nameof(recommendationService));
            _recommendationRepository = recommendationRepository ?? throw new ArgumentNullException(nameof(recommendationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IJwtTokenService _jwtToken;
        private readonly IRecomendationService _recommendationService;
        private readonly IRecommendationRepository _recommendationRepository;
        private readonly ILogger<InterestMediator> _logger;

        public InterestDto Index(ClaimsPrincipal userClaims)
        {
            var userJwtData = this._jwtToken.ReadToken(userClaims);

            var dto = new InterestDto
            {
                Interests = this._categorySubcategoryRepository.GetSubcategoryByInterestAndUserId(userJwtData.UserID),
                Subcategories = this._categorySubcategoryRepository.GetSubcategory(),
                Recommendations = _recommendationRepository.GetSubcategoriesByUserId(userJwtData.UserID)
            };

            return dto;
        }

        public async Task<MessageDto> Create(InterestRequest interestRequest, ClaimsPrincipal userClaims)
        {
            try
            {
                var userJwtData = this._jwtToken.ReadToken(userClaims);

                _recommendationRepository.DeleteAllByUserId(userJwtData.UserID);

                var interests = await _interestRepository.DeleteAllAndCreateAllAsync(interestRequest, userJwtData.UserID);

                foreach (var interest in interests)
                {
                    var recomendationResponse = await _recommendationService.GetAsync(interest.SubcategoryID);

                    foreach (var recommendedSubcategory in recomendationResponse.RecommendedSubcategories)
                    {
                        await _recommendationRepository.AddAsync(interest.InterestID, recommendedSubcategory);
                    }
                }

                foreach (var subcategory in interestRequest.RecommendedSubcategoryID)
                {
                    var recommendation = _recommendationRepository.GetRecommendationByUserIdAndSubcategoryId(userJwtData.UserID, subcategory);

                    if (recommendation != null)
                        _recommendationRepository.Delete(recommendation);
                }

                return GetMessageObject("Os interesses foram salvos com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred while saving user interests at: {ex.StackTrace}");
                return GetMessageObject("Erro ao salvar interesses.");
            }
        }

        private MessageDto GetMessageObject(string message)
        {
            return new MessageDto { Message = message };
        }
    }
}
