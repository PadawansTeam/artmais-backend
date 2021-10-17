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

        [HttpGet("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<PublicationShareLinkDto> GetPublicationShareLinkByPublicationIdAndUserId(long? userId, int? publicationId)
        {
            try
            {
                var result = _publicationService.GetPublicationShareLinkByPublicationIdAndUserId(userId, publicationId);

                return Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return UnprocessableEntity(new { message = ex.Message });
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
        public ActionResult GetIsLikedPublication(int publicationId)
        {
            try
            {
                var user = _jwtToken.ReadToken(User);
                var isLiked = _publicationService.GetIsLikedPublication(publicationId, user.UserID);
                return Ok(isLiked);
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
