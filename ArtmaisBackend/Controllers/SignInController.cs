using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class SignInController : ControllerBase
    {
        public SignInController(ISignInService signIn)
        {
            this._signIn = signIn;
        }

        private readonly ISignInService _signIn;

        [HttpPost]
        public ActionResult<dynamic> Authenticate(SigInRequest sigInRequest)
        {
            try
            {
                return this.Ok(new { token = this._signIn.Authenticate(sigInRequest) });
            }
            catch (Unauthorized ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
