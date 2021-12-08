using ArtmaisBackend.Core.PasswordRecovery.Dtos;
using ArtmaisBackend.Core.PasswordRecovery.Exceptions;
using ArtmaisBackend.Core.PasswordRecovery.Requests;
using ArtmaisBackend.Core.PasswordRecovery.Services;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PasswordRecoveryController : ControllerBase
    {
        private readonly IPasswordRecoveryService _passwordRecoveryService;

        public PasswordRecoveryController(IPasswordRecoveryService passwordRecoveryService)
        {
            _passwordRecoveryService = passwordRecoveryService;
        }

        [HttpPost("[Action]/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PasswordRecoveryDto>> CreateAsync([FromRoute] string email)
        {
            try
            {
                var passwordRecoveryDto = await _passwordRecoveryService.CreateAsync(email);

                return Ok(passwordRecoveryDto);
            }
            catch (UserNotFound ex)
            {
                return NotFound(new MessageDto { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("[Action]/{userId}/{code}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ValidateCodeAsync([FromRoute] long userId, int code)
        {
            try
            {
                var passwordRecovery = await _passwordRecoveryService.ValidateCodeAsync(userId, code);

                return Ok();
            }
            catch (InvalidVerificationCode ex)
            {
                return BadRequest(new MessageDto { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePassword([FromBody] PasswordRecoveryRequest passwordRecoveryRequest)
        {
            try
            {
                _passwordRecoveryService.UpdatePassword(passwordRecoveryRequest);

                return Ok();
            }
            catch (InvalidPassword ex)
            {
                return BadRequest(new MessageDto { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
