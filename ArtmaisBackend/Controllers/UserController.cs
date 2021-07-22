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
        public ActionResult<UserDto> GetLoggedUserInfo()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.GetUserInfoById(user.UserID);
            return this.Ok(result);
        }

        [HttpGet("ShareLink")]
        public ActionResult<ShareLinkDto> GetUserByIdToShareLink([FromQuery] UserRequest userRequest)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.GetShareLink(userRequest, user.UserName);
            return this.Ok(result);
        }

        [HttpGet("ShareProfile")]
        public ActionResult<ShareProfileBaseDto> GetUserByIdToShareProfile([FromQuery] UserRequest userRequest)
        {
            var result = this._userService.GetShareProfile(userRequest);
            return this.Ok(result);
        }
    }
}
