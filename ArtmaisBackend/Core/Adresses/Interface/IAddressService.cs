using ArtmaisBackend.Core.Adresses.Dto;
using ArtmaisBackend.Core.Adresses.Request;

namespace ArtmaisBackend.Core.Adresses.Interface
{
    public interface IAddressService
    {
        AddressDto? CreateOrUpdateUserAddress(AddressRequest? addressRequest, long userId);
        AddressDto? GetAddressByUser(long userId);
    }
}
