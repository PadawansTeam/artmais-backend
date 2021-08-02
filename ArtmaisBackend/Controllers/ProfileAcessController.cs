﻿using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ProfileAcessController : ControllerBase
    {
        public ProfileAcessController(IProfileAcessMediator profileAcessMediator)
        {
            _profileAcessMediator = profileAcessMediator;
        }

        private readonly IProfileAcessMediator _profileAcessMediator;

        [HttpPost]
        [Route("{visitedUserId}")]
        public ActionResult<ProfileAcess> Create([FromRoute] long visitedUserId)
        {
            try
            {
                return Ok(_profileAcessMediator.Create(User, visitedUserId));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
