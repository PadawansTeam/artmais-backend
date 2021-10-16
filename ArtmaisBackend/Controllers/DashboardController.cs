using ArtmaisBackend.Core.Dashboard.Responses;
using ArtmaisBackend.Core.Dashboard.Services;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly ILogger<DashboardController> _logger;
        private readonly IJwtTokenService _jwtTokenService;

        public DashboardController(IDashboardService dashboardService, ILogger<DashboardController> logger, IJwtTokenService jwtTokenService)
        {
            _dashboardService = dashboardService ?? throw new ArgumentNullException(nameof(dashboardService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jwtTokenService = jwtTokenService ?? throw new ArgumentNullException(nameof(jwtTokenService));
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<DashboardResponse>> GetAsync()
        {
            try 
            {
                var user = _jwtTokenService.ReadToken(User);

                return Ok(await _dashboardService.GetAsync(user.UserID));
            }
            catch(Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred while getting dashboard data at: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
