using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace ArtmaisBackend.Controllers
{
    public class UserController : ControllerBase
    {
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private readonly IUserService _userService;

        [HttpGet]
        public ActionResult<int> GetUserById()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            var result = _userService.GetShareLinkAsync(Convert.ToInt32(user.Value));
            return Convert.ToInt32(user.Value);
        }       
    }
}
