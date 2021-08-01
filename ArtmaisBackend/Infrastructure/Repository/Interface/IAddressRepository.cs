using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IAddressRepository
    {
        Address? GetAddressByUser(long? userId);
        Address Create(AddressRequest addressRequest, long userId);
        Address Update(Address address);
    }
}
