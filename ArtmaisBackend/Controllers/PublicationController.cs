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

        [HttpGet("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicationCommentsDto?>> GetAllCommentsByPublicationId(int? publicationId)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                var comments = await _publicationService.GetAllCommentsByPublicationId(publicationId);
                return Ok(comments);
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

        [HttpGet("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PublicationDto>> GetPublicationById(int? publicationId, long? userId)
        {
            try
            {
                var publicationDto = await _publicationService.GetPublicationById(publicationId, userId);
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

        [HttpGet("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int?>> GetAllLikesByPublicationId(int publicationId)
        {
            try
            {
                var likesAmount = await _publicationService.GetAllLikesByPublicationId(publicationId);
                return Ok(likesAmount);
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
