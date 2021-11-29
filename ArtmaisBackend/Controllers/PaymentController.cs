using ArtmaisBackend.Core.Payments.Interface;
using ArtmaisBackend.Core.Payments.Notifications;
using ArtmaisBackend.Core.Payments.Request;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<ActionResult<bool>> PaymentCreateRequest(PaymentRequest paymentRequest)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                var payment = await _paymentService.PaymentCreateRequest(paymentRequest, user.UserID);

                if (payment is null)
                {
                    return UnprocessableEntity(false);
                }

                return Ok(true);
            }
            catch (ArgumentNullException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdatePaymentAsync(PaymentNotification paymentNotification)
        {
            Console.WriteLine(JsonConvert.SerializeObject(paymentNotification));
            return Ok();
        }
    }
}
