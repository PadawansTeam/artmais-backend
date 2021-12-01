using ArtmaisBackend.Core.Mail.Factories;
using ArtmaisBackend.Core.Mail.Requests;
using ArtmaisBackend.Core.Mail.Services;
using ArtmaisBackend.Core.Payments;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }
        [HttpPost("Action")]
        public async Task<IActionResult> SendEmailAsync([FromBody] string email)
        {
            try
            {
                var emailRequest = new EmailRequest
                {
                    ToEmail = email,
                    Subject = PaymentDefaults.PAYMENT_CREATED_SUBJECT,
                    Body = BodyFactory.PaymentBody(PaymentDefaults.PAYMENT_CREATED_MESSAGE)
                };

                await this.mailService.SendEmailAsync(emailRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
