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
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public AddressDto? CreateOrUpdateUserAddress(AddressRequest? addressRequest, long userId)
        {
            if (addressRequest is null)
            {
                throw new ArgumentNullException();
            }

            var addressInfo = _addressRepository.GetAddressByUser(userId);

            if (addressInfo is null)
            {
                var newAddress = _addressRepository.Create(addressRequest, userId);
                if (newAddress is null)
                {
                    throw new ArgumentNullException();
                }


                var addressDto = _mapper.Map<AddressDto>(newAddress);
                return addressDto;
            }
            else
            {
                _mapper.Map(addressRequest, addressInfo);
                var address = _addressRepository.Update(addressInfo);

                var addressDto = _mapper.Map<AddressDto>(address);

                return addressDto;
            }
        }

        public AddressDto? GetAddressByUser(long userId)
        {
            var address = _addressRepository.GetAddressByUser(userId);
            if (address is null)
            {
                throw new ArgumentNullException();
            }

            var addressDto = _mapper.Map<AddressDto>(address);
            return addressDto;
        }
    }
}
