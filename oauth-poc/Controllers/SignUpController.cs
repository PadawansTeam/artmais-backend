﻿using Microsoft.AspNetCore.Mvc;
using oauth_poc.Core.SignUp;
using oauth_poc.Core.SignUp.Interface;
using System;

namespace oauth_poc.Controllers
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

        [HttpPost]
        public ActionResult<dynamic> Create(SignUpRequest signUpRequest)
        {
            try
            {
                return Ok(new { token = _signUp.Create(signUpRequest) });
            }
            catch(Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
