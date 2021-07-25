﻿using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Core.Users.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService, IJwtTokenService jwtToken)
        {
            this._userService = userService;
            this._jwtToken = jwtToken;
        }

        private readonly IUserService _userService;
        private readonly IJwtTokenService _jwtToken;


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> GetLoggedUserInfo()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.GetLoggedUserInfoById(user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> GetUserInfo(int userId)
        {
            var result = this._userService.GetUserInfoById(userId);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpPatch("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> UpdateUserPassword(PasswordRequest passwordRequest)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.UpdateUserPassword(passwordRequest, user.UserID);

            if (result is false)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpPut("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> UpdateUserInfo(UserRequest userRequest)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.UpdateUserInfo(userRequest, user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpGet("ShareLink")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ShareLinkDto> GetUserByIdToShareLink([FromQuery] int id)
        {
            var result = this._userService.GetShareLinkByUserId(id);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpGet("ShareLinkProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ShareLinkDto> GetLoggedUserByIdToShareLink()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._userService.GetShareLinkByLoggedUser(user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpGet("ShareProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ShareProfileBaseDto> GetUserByIdToShareProfile([FromQuery] int id)
        {
            var result = this._userService.GetShareProfile(id);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }
    }
}
