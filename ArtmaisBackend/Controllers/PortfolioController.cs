using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PortfolioController : ControllerBase
    {
        public PortfolioController(IPortfolioService portfolioService, IJwtTokenService jwtToken)
        {
            _portfolioService = portfolioService;
            _jwtToken = jwtToken;
        }

        private readonly IPortfolioService _portfolioService;
        private readonly IJwtTokenService _jwtToken;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PortfolioContentListDto> GetLoggedUserInfo()
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                var result = _portfolioService.GetLoggedUserPortfolioById(user.UserID);

                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PortfolioContentListDto> GetPortfolioByUserId(int userId)
        {
            try
            {
                var result = _portfolioService.GetPortfolioByUserId(userId);
                return Ok(result);

            }
            catch (ArgumentNullException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PortfolioContentDto> InsertPortfolioContent(PortfolioRequest? portfolioRequest)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                var result = _portfolioService.InsertPortfolioContent(portfolioRequest, user.UserID, 1);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpPatch("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> UpdateDescription(PortfolioDescriptionRequest portfolioDescriptionRequest)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                var result = _portfolioService.UpdateDescription(portfolioDescriptionRequest, user.UserID);
                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
