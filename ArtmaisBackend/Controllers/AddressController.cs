using ArtmaisBackend.Core.Adresses.Interface;
using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Controllers
{
    public class AddressController : ControllerBase
    {
        public AddressController(IAddressService addressService, IJwtTokenService jwtToken)
        {
            this._addressService = addressService;
            this._jwtToken = jwtToken;
        }

        private readonly IAddressService _addressService;
        private readonly IJwtTokenService _jwtToken;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<ContactDto> Create(AddressRequest? contactRequest)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._addressService.Create(contactRequest, user.UserID);

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
            var result = this._addressService.GetAddressByUser(user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }
    }
}
