using ArtmaisBackend.Core.Aws.Interface;
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
        public AwsController(IAwsService awsService, IJwtTokenService jwtToken, ILogger<AwsController> logger)
        {
            this._awsService = awsService;
            this._jwtToken = jwtToken;
        }

        private readonly IAwsService _awsService;
        private readonly IJwtTokenService _jwtToken;
        private readonly ILogger _logger;

        [HttpPut("[Action]"), DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> InsertImage()
        {
            try
            {
                var user = this._jwtToken.ReadToken(this.User);
                await this._awsService.UploadObjectAsync(Request.Form.Files[0], user.UserID);
                return this.Ok();
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError($"The error {ex.Message}, occurred while inserting user profile picture at: {ex.StackTrace}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

