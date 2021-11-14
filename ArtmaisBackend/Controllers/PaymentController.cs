﻿using ArtmaisBackend.Core.Payments.Interface;
using ArtmaisBackend.Core.Payments.Request;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PaymentController : ControllerBase
    {
        public PaymentController(IPaymentService paymentService, IJwtTokenService jwtToken)
        {
            this._paymentService = paymentService;
            this._jwtToken = jwtToken;
        }

        private readonly IPaymentService _paymentService;
        private readonly IJwtTokenService _jwtToken;

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> PaymentCreateRequest(PaymentRequest paymentRequest)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                var payment = await _paymentService.PaymentCreateRequest(paymentRequest, user.UserID);

                if (payment is null)
                {
                    return UnprocessableEntity();
                }

                return Ok();
            }
            catch (ArgumentNullException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
