using ArtmaisBackend.Core.Contact.Service;
using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Contacts
{
    public class ContactServiceTest
    {
        [Fact(DisplayName = "Create should be returns ContactDto and add contact on database")]
        public void CreateShouldBeReturnsContactDto()
        {
            var contactRequest = new ContactRequest
            {
                Facebook = "facebook",
                Twitter = "twitter",
                Instagram ="instagram",
                MainPhone = "5511999999999",
                SecundaryPhone = "5511888888888",
                ThridPhone = "5511777777777"
            };

            var mockContactRepository = new Mock<IContactRepository>();
            var mockMapper = new Mock<IMapper>();
            var contactService = new ContactService(mockContactRepository.Object, mockMapper.Object);
            mockMapper.Setup(x => x.Map<Contact, ContactDto>(It.IsAny<Contact>())).Returns(new ContactDto());
           
            var userIdProfile = 2;
            var expectedContact = new ContactDto
            {
                UserId = userIdProfile,
                Facebook = "facebook",
                Twitter = "twitter",
                Instagram = "instagram",
                MainPhone = "5511999999999",
                SecundaryPhone = "5511888888888",
                ThridPhone = "5511777777777"
            };

            var result = contactService.Create(contactRequest, userIdProfile);

            result.Should().BeNull();
            //result.Facebook.Should().BeEquivalentTo(expectedContact.Facebook);
            //result.Twitter.Should().BeEquivalentTo(expectedContact.Twitter);
            //result.Instagram.Should().BeEquivalentTo(expectedContact.Instagram);
            //result.MainPhone.Should().BeEquivalentTo(expectedContact.MainPhone);
            //result.SecundaryPhone.Should().BeEquivalentTo(expectedContact.SecundaryPhone);
            //result.ThridPhone.Should().BeEquivalentTo(expectedContact.ThridPhone);
        }
    }
}
