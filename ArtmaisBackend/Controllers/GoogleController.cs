using ArtmaisBackend.Core.OAuth.Google;
using ArtmaisBackend.Core.OAuth.Google.Interface;
using ArtmaisBackend.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class GoogleController : ControllerBase
    {
        public GoogleController(IGoogleMediator googleMediator)
        {
            _googleMediator = googleMediator;
        }

        private readonly IGoogleMediator _googleMediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("signin/{token}")]
        public async Task<ActionResult<dynamic>> SignIn([FromRoute] string token)
        {
            try
            {
                return Ok(new { token = await _googleMediator.SignIn(token) });
            }
            catch(UserNotFound ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [Route("signup")]
        public ActionResult<dynamic> SignUp([FromBody] OAuthSignUpRequest request)
        {
            try
            {
                return Ok(new { token = _googleMediator.SignUp(request) });
            }
            catch(UsernameAlreadyInUse ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
