using ArtmaisBackend.Core.Aws;
using ArtmaisBackend.Core.Aws.Interface;
using ArtmaisBackend.Core.Aws.Request;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Interface;
using ArtmaisBackend.Core.Portfolio.Request;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Util.File;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
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
            this._logger = logger;
            this._portfolioService = portfolioService;
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
                var uploadObjectCommand = new UploadObjectCommandFactory(user.UserID, this.Request.Form.Files[0], Channel.PROFILE).Create();
                await this._awsService.UploadObjectAsync(uploadObjectCommand);
                return this.Ok();
            }
            catch (InvalidOperationException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this._logger.LogError($"The error {ex.Message}, occurred while updating user profile picture at: {ex.StackTrace}");
                return this.StatusCode(StatusCodes.Status500InternalServerError);
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
                var file = this.Request.Form.Files[0];
                var uploadObjectCommand = new UploadObjectCommandFactory(user.UserID, file, Channel.PORTFOLIO).Create();

                var awsDto = await this._awsService.UploadObjectAsync(uploadObjectCommand);

                var fileExtension = MediaTypeUtil.GetMediaTypeValue(Path.GetExtension(file.FileName));

                var portfolioContentDto = this._portfolioService.InsertPortfolioContent(new PortfolioRequest
                {
                    PortfolioImageUrl = awsDto.Content,
                    Description = this.Request.Form["description"]
                },
                user.UserID,
                (int)fileExtension
                );

                return this.Ok(portfolioContentDto);
            }
            catch (InvalidOperationException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this._logger.LogError($"The error {ex.Message}, occurred while inserting portfolio content at: {ex.StackTrace}");
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{portfolioId}"), DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeletePortfolioContent(int portfolioId)
        {
            try
            {
                var user = this._jwtToken.ReadToken(this.User);
                var deleteObjectCommand = new DeleteObjectCommand(user.UserID, portfolioId);
                var awsDto = await this._awsService.DeletingAnObjectAsync(deleteObjectCommand);

                if (awsDto)
                    return this.Ok(true);
                else
                    return this.BadRequest(false);
            }
            catch (InvalidOperationException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                this._logger.LogError($"The error {ex.Message}, occurred while inserting portfolio content at: {ex.StackTrace}");
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

