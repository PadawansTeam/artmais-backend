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
        [Fact(DisplayName = "Create should be returns null contactdto")]
        public void CreateShouldBeReturnsNullContactDto()
        {
            var contactRequest = new ContactRequest
            {
                Facebook = "facebook",
                Twitter = "twitter",
                Instagram = "instagram",
                MainPhone = "5511999999999",
                SecundaryPhone = "5511888888888",
                ThridPhone = "5511777777777"
            };

            var mockContactRepository = new Mock<IContactRepository>();
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<Contact, ContactDto>(It.IsAny<Contact>())).Returns(new ContactDto());

            var userIdProfile = 2;

            var contactService = new ContactService(mockContactRepository.Object, mockMapper.Object);
            var result = contactService.CreateOrUpdateUserAddress(contactRequest, userIdProfile);

            result.Should().BeNull();
        }
    }
}
