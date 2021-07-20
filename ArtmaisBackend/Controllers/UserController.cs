using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService, IJwtToken jwtToken)
        {
            this._userService = userService;
            this._jwtToken = jwtToken;
        }

        private readonly IUserService _userService;
        private readonly IJwtToken _jwtToken;

        [HttpGet]
        public ActionResult<ShareLinkDto> GetUserById()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.GetShareLinkAsync(user.UserID);
            return this.Ok(result);
        }
    }
}
