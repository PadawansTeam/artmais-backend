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
        public ActionResult<UserDto> GetLoggedUserInfo()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.GetUserInfoById(user.UserID);

            if (result is null)
                return this.UnprocessableEntity();
            else
                return this.Ok(result);
        }

        [HttpGet("ShareLink")]
        public ActionResult<ShareLinkDto> GetUserByIdToShareLink([FromQuery] int id)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.GetShareLink(id, user.UserID);
            if (result is null)
                return this.UnprocessableEntity();
            else
                return this.Ok(result);
        }

        [HttpGet("ShareProfile")]
        public ActionResult<ShareProfileBaseDto> GetUserByIdToShareProfile([FromQuery] int id)
        {
            var result = this._userService.GetShareProfile(id);
            if (result is null)
                return this.UnprocessableEntity();
            else
                return this.Ok(result);
        }
    }
}
