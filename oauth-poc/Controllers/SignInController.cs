using Microsoft.AspNetCore.Mvc;
using oauth_poc.Core.SignIn;
using oauth_poc.Core.SignIn.Interface;
using oauth_poc.Exceptions;

namespace oauth_poc.Controllers
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
