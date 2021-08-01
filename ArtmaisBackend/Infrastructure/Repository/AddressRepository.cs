using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public AddressRepository(ArtplusContext context)
        {
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public Address? GetAddressByUser(int? userId)
        {
            return this._context.Address.FirstOrDefault(address => address.UserID == userId);
        }

        public Address Create(AddressRequest addressRequest, int userId)
        {
            var address = new Address
            {
                UserID = userId,
                Street = addressRequest.Street,
                Number = addressRequest.Number,
                Complement = addressRequest.Complement,
                Neighborhood = addressRequest.Neighborhood,
                ZipCode = addressRequest.ZipCode,
                City = addressRequest.City,
                State = addressRequest.State
            };

            this._context.Address.Add(address);
            this._context.SaveChanges();

            return address;
        }

        public Address Update(Address address)
        {
            this._context.Address.Update(address);
            this._context.SaveChanges();

            return address;
        }
    }
}
