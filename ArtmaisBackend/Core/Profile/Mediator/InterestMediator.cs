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
            IRecomendationService recomendationService,
            IRecomendationRepository recomendationRepository,
            ILogger<InterestMediator> logger)
        {
            _categorySubcategoryRepository = categorySubcategoryRepository ?? throw new ArgumentNullException(nameof(categorySubcategoryRepository));
            _interestRepository = interestRepository ?? throw new ArgumentNullException(nameof(interestRepository));
            _jwtToken = jwtToken ?? throw new ArgumentNullException(nameof(jwtToken));
            _recomendationService = recomendationService ?? throw new ArgumentNullException(nameof(recomendationService));
            _recomendationRepository = recomendationRepository ?? throw new ArgumentNullException(nameof(recomendationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IJwtTokenService _jwtToken;
        private readonly IRecomendationService _recomendationService;
        private readonly IRecomendationRepository _recomendationRepository;
        private readonly ILogger<InterestMediator> _logger;

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

        public async Task<MessageDto> Create(InterestRequest interestRequest, ClaimsPrincipal userClaims)
        {
            try
            {
                var userJwtData = this._jwtToken.ReadToken(userClaims);

                _recomendationRepository.DeleteAllByUserId(userJwtData.UserID);

                var interests = await _interestRepository.DeleteAllAndCreateAllAsync(interestRequest, userJwtData.UserID);

                foreach (var interest in interests)
                {
                    var recomendationResponse = await _recomendationService.GetAsync(interest.SubcategoryID);

                    foreach (var recommendedSubcategory in recomendationResponse.RecommendedSubcategories)
                    {
                        await _recomendationRepository.AddAsync(interest.InterestID, recommendedSubcategory);
                    }
                }

                return GetMessageObject("Os interesses foram salvos com sucesso.");
            }
            catch(Exception ex)
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
