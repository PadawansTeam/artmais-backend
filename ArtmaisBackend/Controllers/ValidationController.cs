using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ValidationController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<ValidationController> _logger;
        private readonly IUserRepository _userRepository;

        public ValidationController(IJwtTokenService jwtTokenService, ILogger<ValidationController> logger, IUserRepository userRepository)
        {
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Validate()
        {
            try
            {
                var userJwtData = _jwtTokenService.ReadToken(User);

                var validateResult = _userRepository.ValidateUserData(userJwtData);

                if (!validateResult)
                    return Unauthorized();

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred while validating user at: {ex.StackTrace}");
                return Unauthorized();
            }
        }
    }
}
