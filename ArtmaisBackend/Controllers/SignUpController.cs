using ArtmaisBackend.Core.SignUp.Dto;
using ArtmaisBackend.Core.SignUp.Interface;
using ArtmaisBackend.Core.SignUp.Request;
using ArtmaisBackend.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SignUpController : ControllerBase
    {
        public SignUpController(ISignUpService signUpService)
        {
            this._signUpService = signUpService;
        }

        private readonly ISignUpService _signUpService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CategorySubcategoryDto> Index()
        {
            return this.Ok(this._signUpService.Index());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<dynamic> Create(SignUpRequest signUpRequest)
        {
            try
            {
                return this.Ok(new { token = this._signUpService.Create(signUpRequest) });
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
