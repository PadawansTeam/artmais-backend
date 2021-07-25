using ArtmaisBackend.Core.Adresses.Dto;
using ArtmaisBackend.Core.Adresses.Interface;
using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.SignIn.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArtmaisBackend.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AddressController : ControllerBase
    {
        public AddressController(IAddressService addressService, IJwtTokenService jwtToken)
        {
            this._addressService = addressService;
            this._jwtToken = jwtToken;
        }

        private readonly IAddressService _addressService;
        private readonly IJwtTokenService _jwtToken;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AddressDto> GetLoggedAddressInfo()
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._addressService.GetAddressByUser(user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpPost("[Action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AddressDto> Upsert(AddressRequest? addressRequest)
        {
            var user = this._jwtToken.ReadToken(this.User);
            var result = this._addressService.CreateOrUpdateUserAddress(addressRequest, user.UserID);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<AddressDto> GetAddressInfoById(int userId)
        {
            var result = this._addressService.GetAddressByUser(userId);

            if (result is null)
                return this.UnprocessableEntity();

            return this.Ok(result);
        }
    }
}
