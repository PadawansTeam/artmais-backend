using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Aws.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AwsController : ControllerBase
    {
        public AwsController(IAwsService awsService, IJwtTokenService jwtToken)
        {
            this._awsService = awsService;
            this._jwtToken = jwtToken;
        }

        private readonly IAwsService _awsService;
        private readonly IJwtTokenService _jwtToken;

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AwsDto> InsertImage([FromForm] IFormFile file)
        {
            try
            {
                var user = this._jwtToken.ReadToken(this.User);
                var result = this._awsService.UploadObjectAsync(file, user.UserID);
                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}

