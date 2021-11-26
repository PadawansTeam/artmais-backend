using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly IUserRepository _userRepostiory;
        private readonly ILogger _logger;

        public SearchController(IUserRepository userRepository, ILogger<SearchController> logger)
        {
            _userRepostiory = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{searchValue}")]
        public ActionResult<IEnumerable<RecommendationDto>> Index([FromRoute] string searchValue)
        {
            try
            {
                var results = _userRepostiory.GetUsersByUsernameOrNameOrSubcategoryOrCategory(searchValue);

                if (results.Any())
                {
                    return Ok(results);
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred when searching for users at: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
