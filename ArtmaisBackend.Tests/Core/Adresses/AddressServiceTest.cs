using ArtmaisBackend.Core.Adresses.Dto;
using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Core.Adresses.Service;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Adresses
{
    public class AddressServiceTest
    {
        [Fact(DisplayName = "Create should be returns AddressDto and add contact on database")]
        public void CreateOrUpdateUserAddressShouldBeReturnsAddressDto()
        {
            var addressRequest = new AddressRequest
            {
                Street = "Street",
                Number = 2,
                Complement = "Complement",
                Neighborhood = "Neighborhood",
                ZipCode = "05860142",
                City = "São Paulo",
                State = "SP"
            };

            var expectedAddress = new AddressDto
            {
                Street = "Street",
                Number = 2,
                Complement = "Complement",
                Neighborhood = "Neighborhood",
                ZipCode = "05860142",
                City = "São Paulo",
                State = "SP"
            };

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockMapper = new Mock<IMapper>();
            mockAddressRepository.Setup(x => x.GetAddressByUser(It.IsAny<int>())).Returns(new Address
            {
                Street = "Street",
                Number = 2,
                Complement = "Complement",
                Neighborhood = "Neighborhood",
                ZipCode = "05860142",
                City = "São Paulo",
                State = "SP"
            });

            mockMapper.Setup(m => m.Map<AddressDto>(It.IsAny<Address>())).Returns(expectedAddress);

            var userIdProfile = 2;
            var contactService = new AddressService(mockAddressRepository.Object, mockMapper.Object);
            var result = contactService.CreateOrUpdateUserAddress(addressRequest, userIdProfile);

            result.Street.Should().BeEquivalentTo(expectedAddress.Street);
            result.Number.Should().Be(expectedAddress.Number);
            result.Complement.Should().BeEquivalentTo(expectedAddress.Complement);
            result.Neighborhood.Should().BeEquivalentTo(expectedAddress.Neighborhood);
            result.ZipCode.Should().BeEquivalentTo(expectedAddress.ZipCode);
        }

        [Fact(DisplayName = "Create should be throw when contactRequest is null or empty")]
        public void CreateOrUpdateUserAddressShouldBeReturnsNullAddressDto()
        {
            var contactRequest = new AddressRequest { };

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<AddressDto>(It.IsAny<Address>())).Returns(new AddressDto());

            var userIdProfile = 2;

            var contactService = new AddressService(mockAddressRepository.Object, mockMapper.Object);
            
            Action act = () => contactService.CreateOrUpdateUserAddress(contactRequest, userIdProfile);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get Address by user should be returns AddressDto")]
        public void GetContactByUserShouldBeReturnsAddressDto()
        {
            var userId = 2;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockMapper = new Mock<IMapper>();

            var expectedUser = new AddressDto
            {
                UserId = 2
            };

            mockAddressRepository.Setup(x => x.GetAddressByUser(It.IsAny<int>())).Returns(new Address
            {
                UserID = 2
            });
            mockMapper.Setup(x => x.Map<AddressDto>(It.IsAny<Address>())).Returns(expectedUser);

            var contactService = new AddressService(mockAddressRepository.Object, mockMapper.Object);

            var result = contactService.GetAddressByUser(userId);

            result.Should().Be(expectedUser);
        }

        [Fact(DisplayName = "Get Address by user should be null AddressDto when userId doesn't exist")]
        public void GetContactByUserShouldBeReturnsNullAddressDto()
        {
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockMapper = new Mock<IMapper>();

            var expectedUser = new AddressDto { };

            mockAddressRepository.Setup(x => x.GetAddressByUser(It.IsAny<int>())).Returns(new Address { });
            mockMapper.Setup(x => x.Map<AddressDto>(It.IsAny<Address>())).Returns(expectedUser);

            var contactService = new AddressService(mockAddressRepository.Object, mockMapper.Object);

            var result = contactService.GetAddressByUser(It.IsAny<int>());

            result.Should().Be(expectedUser);
        }
    }
}
