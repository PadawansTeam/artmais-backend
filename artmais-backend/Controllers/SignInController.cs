﻿using Microsoft.AspNetCore.Mvc;
using artmais_backend.Core.SignIn;
using artmais_backend.Core.SignIn.Interface;
using artmais_backend.Exceptions;

namespace artmais_backend.Controllers
{
    [ApiController]
    [Route("/v1/[controller]")]
    public class SignInController : ControllerBase
    {
        public SignInController(ISignIn signIn)
        {
            _signIn = signIn;
        }

        private readonly ISignIn _signIn;

        [HttpPost]
        public ActionResult<dynamic> Authenticate(SigInRequest sigInRequest)
        {
            try
            {
                return Ok(new { token = _signIn.Authenticate(sigInRequest) });
            }
            catch (Unauthorized ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}