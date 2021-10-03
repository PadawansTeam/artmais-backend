using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProfileAccessController : ControllerBase
    {
        public ProfileAccessController(IProfileAccessMediator profileAcessMediator)
        {
            _profileAcessMediator = profileAcessMediator;
        }

        private readonly IProfileAccessMediator _profileAcessMediator;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{visitedUserId}")]
        public ActionResult Create([FromRoute] long visitedUserId)
        {
            try
            {
                _profileAcessMediator.Create(User, visitedUserId);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
