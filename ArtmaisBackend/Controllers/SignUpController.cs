using Microsoft.AspNetCore.Mvc;
using ArtmaisBackend.Core.SignUp;
using ArtmaisBackend.Core.SignUp.Interface;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SignUpController : ControllerBase
    {
        public SignUpController(ISignUp signUp)
        {
            _signUp = signUp;
        }

        private readonly ISignUp _signUp;

        [HttpGet]
        public ActionResult<CategorySubcategoryDto> Index()
        {
            return Ok(_signUp.Index());
        }

        [HttpPost]
        public ActionResult<dynamic> Create(SignUpRequest signUpRequest)
        {
            try
            {
                return Ok(new { token = _signUp.Create(signUpRequest) });
            }
            catch(EmailAlreadyInUse ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
