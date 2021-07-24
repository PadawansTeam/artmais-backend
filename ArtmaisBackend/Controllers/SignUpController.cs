using ArtmaisBackend.Core.SignUp.Dto;
using ArtmaisBackend.Core.SignUp.Interface;
using ArtmaisBackend.Core.SignUp.Request;
using ArtmaisBackend.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SignUpController : ControllerBase
    {
        public SignUpController(ISignUp signUp)
        {
            this._signUp = signUp;
        }

        private readonly ISignUp _signUp;

        [HttpGet]
        public ActionResult<CategorySubcategoryDto> Index()
        {
            return this.Ok(this._signUp.Index());
        }

        [HttpPost]
        public ActionResult<dynamic> Create(SignUpRequest signUpRequest)
        {
            try
            {
                return this.Ok(new { token = this._signUp.Create(signUpRequest) });
            }
            catch (EmailAlreadyInUse ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (UsernameAlreadyInUse ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
