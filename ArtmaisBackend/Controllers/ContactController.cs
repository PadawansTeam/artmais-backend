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
        public ContactController(IContactService contact, IJwtToken jwtToken)
        {
            this._contact = contact;
            this._jwtToken = jwtToken;
        }

        private readonly IContactService _contact;
        private readonly IJwtToken _jwtToken;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> Create(ContactRequest? contactRequest)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._contact.Create(contactRequest, user.UserID);
            
            if (result is null)
                return this.UnprocessableEntity();
            
            return this.Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> GetContactInfoById()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._contact.GetContactByUser(user.UserID);
            
            if (result is null)
                return this.UnprocessableEntity();
           
            return this.Ok(result);
        }
    }
}
