using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.SignIn.Service;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Core.Users.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        private readonly IUserService _userService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> GetLoggedUserInfo()
        {
            try
            {
                var user = JwtTokenUtil.ReadToken(this.User);
                var result = this._userService.GetLoggedUserInfoById(user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserDto> GetUserInfo(int userId)
        {
            try
            {
                var result = this._userService.GetUserInfoById(userId);
                return this.Ok(result);

            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpPatch("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> UpdateUserPassword(PasswordRequest passwordRequest)
        {
            try
            {
                var user = JwtTokenUtil.ReadToken(this.User);
                var result = this._userService.UpdateUserPassword(passwordRequest, user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
            catch (AccessViolationException)
            {
                return Forbid();
            }
        }

        [HttpPatch("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> UpdateUserDescription(UserDescriptionRequest descriptionRequest)
        {
            try
            {
                var user = JwtTokenUtil.ReadToken(this.User);
                var result = this._userService.UpdateUserDescription(descriptionRequest, user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpPut("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<UserProfileInfoDto> UpdateUserInfo(UserRequest userRequest)
        {
            try
            {
                var user = JwtTokenUtil.ReadToken(this.User);
                var result = this._userService.UpdateUserInfo(userRequest, user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpGet("ShareLink")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ShareLinkDto> GetUserByIdToShareLink([FromQuery] int id)
        {
            try
            {
                var result = this._userService.GetShareLinkByUserId(id);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpGet("ShareLinkProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ShareLinkDto> GetLoggedUserByIdToShareLink()
        {
            try
            {
                var user = JwtTokenUtil.ReadToken(this.User);
                var result = this._userService.GetShareLinkByLoggedUser(user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpGet("ShareProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ShareProfileBaseDto> GetUserByIdToShareProfile([FromQuery] int id)
        {
            try
            {
                var result = this._userService.GetShareProfile(id);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
