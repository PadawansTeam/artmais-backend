using ArtmaisBackend.Core.OAuth.Google.Interface;
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
        [Route("login/{token}")]
        public async Task<ActionResult<bool>> Login([FromRoute] string token)
        {
            return Ok(await _googleMediator.SignIn(token));
        }
    }
}
