using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Publications.Dto;
using ArtmaisBackend.Core.Publications.Interface;
using ArtmaisBackend.Core.Publications.Request;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class PublicationController : ControllerBase
    {
        public PublicationController(IPublicationService publicationService, IJwtTokenService jwtToken)
        {
            _publicationService = publicationService;
            _jwtToken = jwtToken;
        }

        private readonly IPublicationService _publicationService;
        private readonly IJwtTokenService _jwtToken;

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult InsertComment(CommentRequest? commentRequest)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                _publicationService.InsertComment(commentRequest, user.UserID);
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

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult InsertLike(int publicationId)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                _publicationService.InsertLike(publicationId, user.UserID);
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

        [HttpDelete("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteLike(int publicationId)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                _publicationService.DeleteLike(publicationId, user.UserID);
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

        [HttpDelete("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteComment(int commentId)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                _publicationService.DeleteComment(commentId, user.UserID);
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

        [HttpGet("[Action]/{publicationId}/{publicationOwnerUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicationDto>> GetPublicationByIdAndLoggedUser(int? publicationId, long? publicationOwnerUserId)
        {
            try
            {
                var visitorUser = _jwtToken.ReadToken(User);
                var publicationDto = await _publicationService.GetPublicationByIdAndLoggedUser(publicationId, publicationOwnerUserId, visitorUser);
                return Ok(publicationDto);
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

        [HttpGet("[Action]/{publicationId}/{publicationOwnerUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicationDto>> GetPublicationById(int? publicationId, long? publicationOwnerUserId)
        {
            try
            {
                var publicationDto = await _publicationService.GetPublicationById(publicationId, publicationOwnerUserId);
                return Ok(publicationDto);
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
