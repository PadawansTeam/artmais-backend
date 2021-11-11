using ArtmaisBackend.Core.Signatures.Dto;
using ArtmaisBackend.Core.Signatures.Interface;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class SignatureController : ControllerBase
    {
        public SignatureController(ISignatureService signatureService, IJwtTokenService jwtToken)
        {
            _signatureService = signatureService ?? throw new ArgumentNullException(nameof(signatureService)); ;
            _jwtToken = jwtToken ?? throw new ArgumentNullException(nameof(jwtToken)); ;
        }

        private readonly ISignatureService _signatureService;
        private readonly IJwtTokenService _jwtToken;

        [HttpGet("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SignatureDto>> GetSignatureUserDto()
        {
            try
            {
                var user = _jwtToken.ReadToken(this.User);
                var result = await _signatureService.GetSignatureUserDto(user.UserID);
                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

    }
}
