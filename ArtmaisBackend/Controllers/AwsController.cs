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
            _awsService = awsService;
            _jwtToken = jwtToken;
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
                var user = _jwtToken.ReadToken(User);
                var uploadObjectCommand = UploadObjectCommandFactory.Create(user.UserID, Request.Form.Files[0], Channel.PROFILE);
                await _awsService.UploadObjectAsync(uploadObjectCommand);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
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
                var user = _jwtToken.ReadToken(User);
                var file = Request.Form.Files[0];
                var uploadObjectCommand = UploadObjectCommandFactory.Create(user.UserID, file, Channel.PORTFOLIO);

                var awsDto = await _awsService.UploadObjectAsync(uploadObjectCommand);

                var fileExtension = MediaTypeUtil.GetMediaTypeValue(Path.GetExtension(file.FileName));

                var portfolioContentDto = _portfolioService.InsertPortfolioContent(new PortfolioRequest
                {
                    PortfolioImageUrl = awsDto.Content,
                    Description = Request.Form["description"]
                },
                user.UserID,
                (int)fileExtension
                );

                return Ok(portfolioContentDto);
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred while inserting portfolio content at: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("[Action]/{contentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeletePortfolioContent([FromRoute] int contentId)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                var deleteObjectCommand = new DeleteObjectCommand(user.UserID, contentId);
                var awsDto = await _awsService.DeletingAnObjectAsync(deleteObjectCommand);

                if (awsDto)
                {
                    return Ok(true);
                }
                else
                {
                    return BadRequest(false);
                }
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred while deleting portfolio content at: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

