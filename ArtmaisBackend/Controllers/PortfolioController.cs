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
            this._portfolioService = portfolioService;
            this._jwtToken = jwtToken;
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
                var user = this._jwtToken.ReadToken(this.User);
                var result = this._portfolioService.GetLoggedUserPortfolioById(user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
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
                var result = this._portfolioService.GetPortfolioByUserId(userId);
                return this.Ok(result);

            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
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
                var user = this._jwtToken.ReadToken(this.User);
                var result = this._portfolioService.InsertPortfolioContent(portfolioRequest, user.UserID, 1);
                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
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
                var user = this._jwtToken.ReadToken(this.User);
                var result = this._portfolioService.UpdateDescription(portfolioDescriptionRequest, user.UserID);
                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}/{portfolioId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PortfolioContentListDto> GetPortfolioContentById(int portfolioId, int userId)
        {
            try
            {
                var result = this._portfolioService.GetPortfolioContentById(portfolioId, userId);
                return this.Ok(result);

            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
