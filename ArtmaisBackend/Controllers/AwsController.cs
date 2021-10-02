using ArtmaisBackend.Core.Aws;
using ArtmaisBackend.Core.Aws.Interface;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Request;
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
    public class AwsController : ControllerBase
    {
        public AwsController(IAwsService awsService, IJwtTokenService jwtToken, ILogger<AwsController> logger, IPortfolioService portfolioService)
        {
            this._awsService = awsService;
            this._jwtToken = jwtToken;
            _logger = logger;
            _portfolioService = portfolioService;
        }

        private readonly IAwsService _awsService;
        private readonly IJwtTokenService _jwtToken;
        private readonly ILogger _logger;
        private readonly IPortfolioService _portfolioService;

        [HttpPut("[Action]"), DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> UpdateProfilePicture()
        {
            try
            {
                var user = this._jwtToken.ReadToken(this.User);
                var uploadObjectCommand = new UploadObjectCommandFactory(user.UserID, Request.Form.Files[0], Channel.PROFILE).Create();
                await this._awsService.UploadObjectAsync(uploadObjectCommand);
                return this.Ok();
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred while updating user profile picture at: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("[Action]"), DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PortfolioContentDto>> InsertPortfolioContent()
        {
            try
            {
                var user = this._jwtToken.ReadToken(this.User);
                var uploadObjectCommand = new UploadObjectCommandFactory(user.UserID, Request.Form.Files[0], Channel.PORTFOLIO).Create();

                var awsDto = await this._awsService.UploadObjectAsync(uploadObjectCommand);

                var portfolioContentDto = _portfolioService.InsertPortfolioContent(new PortfolioRequest
                {
                    PortfolioImageUrl = awsDto.Content,
                    Description = Request.Form["description"]
                },
                user.UserID,
                1
                );

                return this.Ok(portfolioContentDto);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred while inserting portfolio content at: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

