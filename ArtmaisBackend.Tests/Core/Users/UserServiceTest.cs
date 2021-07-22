using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Users.Request;
using ArtmaisBackend.Core.Users.Service;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Users
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "Should be returns ShareLinkDto when userName with userName is equals userNameProfile")]
        public void GetShareLinkShouldBeNameReturnsShareLinkDtoWithUserNameAndUserNameProfileItIsEquals()
        {
            var request = new UserRequest
            {
                Username = "userName"
            };

            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetUserByUsername((It.IsAny<string>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<int>()))).Returns(new Contact { });


            var url = mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration
            {
                Facebook = "https://www.facebook.com/sharer/sharer.php?u=",
                Twitter = "https://twitter.com/intent/tweet?text=",
                Whatsapp = "https://wa.me/",
                ArtMais = "https://artmais-frontend.herokuapp.com/"
            }
            );

            var userNameProfile = "userName";
            var userService = new UserService(mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object);

            var result = userService.GetShareLink(request, userNameProfile);

            result.Twitter.Should().BeEquivalentTo("https://twitter.com/intent/tweet?text=https://artmais-frontend.herokuapp.com/userName%20Este%20é%20meu%20perfil%20na%20Plataforma%20Art%2B,%20visiti-o%20para%20conhecer%20o%20meu%20trabalho.");
            result.Facebook.Should().BeEquivalentTo("https://www.facebook.com/sharer/sharer.php?u=https://artmais-frontend.herokuapp.com/userName%20Este%20é%20meu%20perfil%20na%20Plataforma%20Art%2B,%20visiti-o%20para%20conhecer%20o%20meu%20trabalho.");
            result.Whatsapp.Should().BeEquivalentTo("https://wa.me/text=https://artmais-frontend.herokuapp.com/userName%20Este%20é%20meu%20perfil%20na%20Plataforma%20Art%2B,%20visiti-o%20para%20conhecer%20o%20meu%20trabalho.");
            result.WhatsappContact.Should().BeNullOrEmpty();
            result.Instagram.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "Should be returns ShareLinkDto when userName it is not equals userNameProfile")]
        public void GetShareLinkShouldBeReturnsShareLinkDtoWithUserNameAndUserNameProfileItIsNotEquals()
        {
            var request = new UserRequest
            {
                Username = "userName"
            };

            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetUserByUsername((It.IsAny<string>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<int>()))).Returns(new Contact
            {
                MainPhone = "5511984439282"
            });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration
            {
                Facebook = "https://www.facebook.com/sharer/sharer.php?u=",
                Twitter = "https://twitter.com/intent/tweet?text=",
                Whatsapp = "https://wa.me/",
                ArtMais = "https://artmais-frontend.herokuapp.com/"
            }
            );

            var userNameProfile = "userNameProfile";
            var userService = new UserService(mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object);

            var result = userService.GetShareLink(request, userNameProfile);

            result.Twitter.Should().BeEquivalentTo("https://twitter.com/intent/tweet?text=https://artmais-frontend.herokuapp.com/userNameProfile%20Olhá%20só%20que%20perfil%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
            result.Facebook.Should().BeEquivalentTo("https://www.facebook.com/sharer/sharer.php?u=https://artmais-frontend.herokuapp.com/userNameProfile%20Olhá%20só%20que%20perfil%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
            result.Whatsapp.Should().BeEquivalentTo("https://wa.me/text=https://artmais-frontend.herokuapp.com/userNameProfile%20Olhá%20só%20que%20perfil%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
            result.WhatsappContact.Should().BeEquivalentTo("https://wa.me/?phone=5511984439282&text=+Olá,+gostaria+de+conversar+sobre+a+sua+arte+disponível+na+plataforma+Art%2B.");
            result.Instagram.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "Should be GetShareLink throw when request is null or empty")]
        public void GetShareLinkShouldBeThrow()
        {
            var request = new UserRequest { };

            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetUserByUsername((It.IsAny<string>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<int>()))).Returns(new Contact { });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration { });

            var userService = new UserService(mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object);

            Action act = () => userService.GetShareProfile(request);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = "Should be returns ShareProfileDto based on userId")]
        public void GetShareProfileShouldBeReturnsShareLinkDto()
        {
            var request = new UserRequest
            {
                Username = "Username"
            };

            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetUserByUsername((It.IsAny<string>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<int>()))).Returns(new Contact
            {
                Twitter = "TwitterUserName",
                Facebook = "FacebookUserName",
                Instagram = "InstagramUserName",
            });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration
            {
                FacebookProfile = "https://www.facebook.com/",
                TwitterProfile = "https://twitter.com/",
                InstagramProfile = "https://www.instagram.com/"
            }
            );

            var userService = new UserService(mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object);

            var result = userService.GetShareProfile(request);

            result.Twitter.Should().BeEquivalentTo("https://twitter.com/TwitterUserName");
            result.Facebook.Should().BeEquivalentTo("https://www.facebook.com/FacebookUserName");
            result.Instagram.Should().BeEquivalentTo("https://www.instagram.com/InstagramUserName");
        }

        [Fact(DisplayName = "Should be GetShareProfile throw when request is null or empty")]
        public void GetShareProfileShouldBeThrow()
        {
            var request = new UserRequest { };

            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();

            mockUserRepository.Setup(x => x.GetUserByUsername((It.IsAny<string>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<int>()))).Returns(new Contact { });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration { });

            var userService = new UserService(mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object);

            Action act = () => userService.GetShareProfile(request);
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
