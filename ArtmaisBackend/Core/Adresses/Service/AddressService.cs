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

        public AddressDto? CreateOrUpdateUserContact(AddressRequest? addressRequest, int userId)
        {
            if (addressRequest is null) return null;

            var addressInfo = this._addressRepository.GetAddressByUser(userId);

            if (addressInfo is null)
            {
                var newAddress = this._addressRepository.Create(addressRequest, userId);
                if (newAddress is null) return null;

                var addressDto = this._mapper.Map<AddressDto>(newAddress);

                return addressDto;
            }
            else
            {
                this._mapper.Map(addressRequest, addressInfo);
                var address = this._addressRepository.Update(addressInfo);

                var addressDto = this._mapper.Map<AddressDto>(address);

                return addressDto;
            }
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
