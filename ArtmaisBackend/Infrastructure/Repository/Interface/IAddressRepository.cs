using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IAddressRepository
    {
        Address? GetAddressByUser(int? userId);
        Address Create(AddressRequest addressRequest, int userId);
        Address Update(Address address);
    }
}
