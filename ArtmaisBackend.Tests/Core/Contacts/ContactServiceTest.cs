using ArtmaisBackend.Core.Contact.Service;
using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Contacts
{
    public class ContactServiceTest
    {
        [Fact(DisplayName = "Create should be returns ContactDto and add contact on database")]
        public void CreateOrUpdateUserContactShouldBeReturnsContactDto()
        {
            var contactRequest = new ContactRequest
            {
                Facebook = "facebook",
                Twitter = "twitter",
                Instagram = "instagram",
                MainPhone = "5511999999999",
                SecundaryPhone = "5511888888888",
                ThirdPhone = "5511777777777"
            };

            var expectedContact = new ContactDto
            {
                Facebook = "facebook",
                Twitter = "twitter",
                Instagram = "instagram",
                MainPhone = "5511999999999",
                SecundaryPhone = "5511888888888",
                ThirdPhone = "5511777777777"
            };

            var mockContactRepository = new Mock<IContactRepository>();
            var mockMapper = new Mock<IMapper>();
            mockContactRepository.Setup(x => x.GetContactByUser(It.IsAny<int>())).Returns(new Contact
            {
                UserID = 2,
                Facebook = "facebook",
                Twitter = "twitter",
                Instagram = "instagram",
                MainPhone = "5511999999999",
                SecundaryPhone = "5511888888888",
                ThirdPhone = "5511777777777"
            });
            mockMapper.Setup(m => m.Map<ContactDto>(It.IsAny<Contact>())).Returns(expectedContact);

            var userIdProfile = 2;
            var contactService = new ContactService(mockContactRepository.Object, mockMapper.Object);
            var result = contactService.CreateOrUpdateUserContact(contactRequest, userIdProfile);

            result.Facebook.Should().BeEquivalentTo(expectedContact.Facebook);
            result.Twitter.Should().BeEquivalentTo(expectedContact.Twitter);
            result.Instagram.Should().BeEquivalentTo(expectedContact.Instagram);
            result.MainPhone.Should().BeEquivalentTo(expectedContact.MainPhone);
            result.SecundaryPhone.Should().BeEquivalentTo(expectedContact.SecundaryPhone);
            result.ThirdPhone.Should().BeEquivalentTo(expectedContact.ThirdPhone);
        }
         
        [Fact(DisplayName = "Create should be null contactdto when contactRequest is null or empty")]
        public void CreateOrUpdateUserContactShouldBeReturnsNullContactDto()
        {
            var contactRequest = new ContactRequest{};

            var mockContactRepository = new Mock<IContactRepository>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<ContactDto>(It.IsAny<Contact>())).Returns(new ContactDto());

            var userIdProfile = 2;

            var contactService = new ContactService(mockContactRepository.Object, mockMapper.Object);
            
            Action act = () => contactService.CreateOrUpdateUserContact(contactRequest, userIdProfile);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "Get contact by user should be returns contactdto")]
        public void GetContactByUserShouldBeReturnsContactDto()
        {
            var userId = 2;

            var mockContactRepository = new Mock<IContactRepository>();
            var mockMapper = new Mock<IMapper>();

            var expectedUser = new ContactDto
            {
                UserId = 2
            };

            mockContactRepository.Setup(x => x.GetContactByUser(It.IsAny<int>())).Returns(new Contact
            {
                UserID = 2
            });
            mockMapper.Setup(x => x.Map<ContactDto>(It.IsAny<Contact>())).Returns(expectedUser);

            var contactService = new ContactService(mockContactRepository.Object, mockMapper.Object);

            var result = contactService.GetContactByUser(userId);

            result.Should().Be(expectedUser);
        }

        [Fact(DisplayName = "Get contact by user should be null when contact doesn't exist")]
        public void GetContactByUserShouldBeReturnsNullContactDto()
        {
            var mockContactRepository = new Mock<IContactRepository>();
            var mockMapper = new Mock<IMapper>();

            var expectedUser = new ContactDto { };

            mockContactRepository.Setup(x => x.GetContactByUser(It.IsAny<int>())).Returns(new Contact { });
            mockMapper.Setup(x => x.Map<ContactDto>(It.IsAny<Contact>())).Returns(expectedUser);

            var contactService = new ContactService(mockContactRepository.Object, mockMapper.Object);

            var result = contactService.GetContactByUser(It.IsAny<int>());

            result.Should().Be(expectedUser);
        }
    }
}
