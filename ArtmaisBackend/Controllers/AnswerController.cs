using ArtmaisBackend.Core.Answer.Requests;
using ArtmaisBackend.Core.Answer.Services;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IJwtTokenService _jwtTokenService;

        public AnswerController(IAnswerService answerService, IJwtTokenService jwtTokenService)
        {
            _answerService = answerService;
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Answer>> CreateAsync([FromBody] AnswerRequest answerRequest)
        {
            try
            {
                var user = _jwtTokenService.ReadToken(User);
                var answer = await _answerService.CreateAsync(answerRequest, user.UserID);

                return Ok(answer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new MessageDto { Message = ex.Message });
            }
        }

        [HttpDelete("[Action]/{answerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Answer>> DeleteAsync([FromRoute] long answerId)
        {
            try
            {
                var user = _jwtTokenService.ReadToken(User);
                var answer = await _answerService.DeleteAsync(answerId, user.UserID);

                return Ok(answer);
            }
            catch (Unauthorized ex)
            {
                return Unauthorized(new MessageDto { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new MessageDto { Message = ex.Message });
            }
        }
    }
}
