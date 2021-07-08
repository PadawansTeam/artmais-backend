using Microsoft.AspNetCore.Mvc;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Exceptions;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class SignInController : ControllerBase
    {
        public SignInController(ISignIn signIn)
        {
            _signIn = signIn;
        }

        private readonly ISignIn _signIn;

        [HttpPost]
        public ActionResult<dynamic> Authenticate(SigInRequest sigInRequest)
        {
            try
            {
                return Ok(new { token = _signIn.Authenticate(sigInRequest) });
            }
            catch (Unauthorized ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
