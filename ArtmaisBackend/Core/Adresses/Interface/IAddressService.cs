using ArtmaisBackend.Core.Adresses.Dto;
using ArtmaisBackend.Core.Adresses.Request;

namespace ArtmaisBackend.Core.Adresses.Interface
{
    public interface IAddressService
    {
        AddressDto? Create(AddressRequest? addressRequest, int userId);
        AddressDto? GetAddressByUser(int userId);
    }
}
