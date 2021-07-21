using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Service;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Users
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "Should be returns ShareLinkDto based on userId and userName")]
        public async Task userIdAndUserNameReturnsShareLinkDto()
        {
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();

            mockContactRepository.Setup(x => x.GetContactByUserAsync((It.IsAny<int>()))).ReturnsAsync(new Contact 
            {
                Facebook = "facebook",
                Twitter = "twitter",
                Instagram = "instagram",
                MainPhone = "5511984433982",
                SecundaryPhone = "secondaryPhone",
                ThirdPhone = "thirdPhone"
            });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration 
            { 
                Facebook = "https://www.facebook.com/sharer/sharer.php?u=",
                Twitter = "https://twitter.com/intent/tweet?text=",
                Whatsapp = "https://wa.me/",
                ArtMais = "https://artmais-frontend.herokuapp.com/"
            }
            );

            var userNameTest = "userNameTest";
            var userService = new UserService(mockContactRepository.Object, mockOptions.Object);

            var result = await userService.GetShareLinkAsync(3, userNameTest);

            result.Twitter.Should().Contain(userNameTest);
            result.Facebook.Should().BeEquivalentTo("https://www.facebook.com/sharer/sharer.php?u=https://artmais-frontend.herokuapp.com/userNameTest%20Venha%20apreciar%20e%20se%20deslumbrar%20com%20estás%20a%20artes%20disponíveis%20na%20plataforma%20Art%2B.");
            result.Whatsapp.Should().Contain(userNameTest);
            result.WhatsappContact.Should().Contain(userNameTest);
            result.Instagram.Should().BeEmpty();
        }
    }
}
