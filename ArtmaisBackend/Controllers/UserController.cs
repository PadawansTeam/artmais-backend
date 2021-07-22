using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Core.Users.Request;
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
        public ActionResult<ShareLinkDto> GetUserByIdToShareLink(UsernameRequest usernameRequest)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.GetShareLink(usernameRequest, user.UserName);
            return this.Ok(result);
        }

        [HttpGet]
        public ActionResult<ShareLinkDto> GetUserByIdToShareProfile(UsernameRequest usernameRequest)
        {
            var result = this._userService.GetShareProfile(usernameRequest);
            return this.Ok(result);
        }
    }
}
