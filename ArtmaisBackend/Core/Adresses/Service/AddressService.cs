using ArtmaisBackend.Core.Adresses.Dto;
using ArtmaisBackend.Core.Adresses.Interface;
using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;

namespace ArtmaisBackend.Core.Adresses.Service
{
    public class AddressService : IAddressService
    {
        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            this._addressRepository = addressRepository;
            this._mapper = mapper;
        }

        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressDto? Create(AddressRequest? addressRequest, int userId)
        {
            if (addressRequest is null) return null;

            var address = this._addressRepository.Create(addressRequest, userId);
            if (address is null) return null;

            var addressDto = this._mapper.Map<AddressDto>(address);

            return addressDto;
        }

        public AddressDto? GetAddressByUser(int userId)
        {
            var address = this._addressRepository.GetAddressByUser(userId);
            if (address is null) return null;

            var addressDto = this._mapper.Map<AddressDto>(address);
            return addressDto;
        }
    }
}
