using ArtmaisBackend.Core.Adresses.Dto;
using ArtmaisBackend.Core.Adresses.Interface;
using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using System;

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

        public AddressDto? CreateOrUpdateUserAddress(AddressRequest? addressRequest, long userId)
        {
            if (addressRequest is null) 
                throw new ArgumentNullException();

            var addressInfo = this._addressRepository.GetAddressByUser(userId);

            if (addressInfo is null)
            {
                var newAddress = this._addressRepository.Create(addressRequest, userId);
                if (newAddress is null)
                    throw new ArgumentNullException();


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

        public AddressDto? GetAddressByUser(long userId)
        {
            var address = this._addressRepository.GetAddressByUser(userId);
            if (address is null)
                throw new ArgumentNullException();

            var addressDto = this._mapper.Map<AddressDto>(address);
            return addressDto;
        }
    }
}
