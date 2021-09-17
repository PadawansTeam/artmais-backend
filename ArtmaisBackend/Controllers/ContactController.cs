using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Interface;
using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.SignIn.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ContactController : ControllerBase
    {
        public ContactController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        private readonly IContactService _contactService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> GetLoggedContactInfo()
        {
            try
            {
                var user = JwtTokenService.ReadToken(this.User);
                var result = this._contactService.GetContactByUser(user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> Upsert(ContactRequest? contactRequest)
        {
            try
            {
                var user = JwtTokenService.ReadToken(this.User);
                var result = this._contactService.CreateOrUpdateUserContact(contactRequest, user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> GetContactInfoById()
        {
            try
            {
                var user = JwtTokenService.ReadToken(this.User);
                var result = this._contactService.GetContactByUser(user.UserID);

                return this.Ok(result);
            }
            catch (ArgumentNullException ex)
            {
                return this.UnprocessableEntity(new { message = ex.Message });
            }
        }
    }
}
