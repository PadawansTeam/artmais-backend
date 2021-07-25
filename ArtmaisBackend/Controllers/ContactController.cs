using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Interface;
using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class ContactController : ControllerBase
    {
        public ContactController(IContactService contactService, IJwtTokenService jwtToken)
        {
            this._contactService = contactService;
            this._jwtToken = jwtToken;
        }

        private readonly IContactService _contactService;
        private readonly IJwtTokenService _jwtToken;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> GetLoggedContactInfo()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._contactService.GetContactByUser(user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> Upsert(ContactRequest? contactRequest)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._contactService.CreateOrUpdateUserAddress(contactRequest, user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> GetContactInfoById()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._contactService.GetContactByUser(user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }
    }
}
