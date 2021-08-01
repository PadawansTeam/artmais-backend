using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Request;
using ArtmaisBackend.Core.Users.Service;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace ArtmaisBackend.Tests.Core.Users
{
    public class UserServiceTest
    {
        [Fact(DisplayName = "GetShareLinkByLoggedUser should be returns ShareLinkDto when userId is equals userIdProfile")]
        public void GetShareLinkByLoggedUserReturnsShareLinkDtoWithUserIdAndUserIdProfileItIsEquals()
        {
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(new User
            {
                Username = "userName",
                UserID = 3
            });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<long>()))).Returns(new Contact { });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration
            {
                Facebook = "https://www.facebook.com/sharer/sharer.php?u=",
                Twitter = "https://twitter.com/intent/tweet?text=",
                Whatsapp = "https://wa.me/",
                ArtMais = "https://artmais-frontend.herokuapp.com/artista/"
            }
            );

            var userIdProfile = 3;
            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            var result = userService.GetShareLinkByLoggedUser(userIdProfile);

            result.Twitter.Should().BeEquivalentTo("https://twitter.com/intent/tweet?text=https://artmais-frontend.herokuapp.com/artista/3%20Este%20é%20meu%20perfil%20na%20Plataforma%20Art%2B,%20visite-o%20para%20conhecer%20o%20meu%20trabalho.");
            result.Facebook.Should().BeEquivalentTo("https://www.facebook.com/sharer/sharer.php?u=https://artmais-frontend.herokuapp.com/artista/3%20Este%20é%20meu%20perfil%20na%20Plataforma%20Art%2B,%20visite-o%20para%20conhecer%20o%20meu%20trabalho.");
            result.Whatsapp.Should().BeEquivalentTo("https://wa.me/?text=https://artmais-frontend.herokuapp.com/artista/3%20Este%20é%20meu%20perfil%20na%20Plataforma%20Art%2B,%20visite-o%20para%20conhecer%20o%20meu%20trabalho.");
            result.WhatsappContact.Should().BeNullOrEmpty();
            result.Instagram.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "GetShareLinkByLoggedUser should be null when request is null or empty")]
        public void GetShareLinkByLoggedUserReturnsNullWhenUserIdIsNullOrEmpty()
        {
            int? userIdProfile = null;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<long>()))).Returns(new Contact { });

            var url = mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration { });

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            Action act = () => userService.GetShareLinkByLoggedUser(userIdProfile);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "GetShareLinkByUserId should be returns ShareLinkDto when userId it is not equals userIdProfile")]
        public void GetShareLinkByUserIdReturnsShareLinkDtoWithUserIdAndUserIdProfileItIsNotEquals()
        {
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(new User
            {
                UserID = 3
            });


            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<long>()))).Returns(new Contact
            {
                MainPhone = "5511984439282"
            });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration
            {
                Facebook = "https://www.facebook.com/sharer/sharer.php?u=",
                Twitter = "https://twitter.com/intent/tweet?text=",
                Whatsapp = "https://wa.me/",
                ArtMais = "https://artmais-frontend.herokuapp.com/artista/"
            }
            );

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            var result = userService.GetShareLinkByUserId(3);

            result.Twitter.Should().BeEquivalentTo("https://twitter.com/intent/tweet?text=https://artmais-frontend.herokuapp.com/artista/3%20Olhá%20só%20que%20perfil%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
            result.Facebook.Should().BeEquivalentTo("https://www.facebook.com/sharer/sharer.php?u=https://artmais-frontend.herokuapp.com/artista/3%20Olhá%20só%20que%20perfil%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
            result.Whatsapp.Should().BeEquivalentTo("https://wa.me/?text=https://artmais-frontend.herokuapp.com/artista/3%20Olhá%20só%20que%20perfil%20incrivel%20que%20eu%20achei%20na%20plataforma%20Art%2B.");
            result.WhatsappContact.Should().BeEquivalentTo("https://wa.me/?phone=5511984439282&text=+Olá,+gostaria+de+conversar+sobre+a+sua+arte+disponível+na+plataforma+Art%2B.");
            result.Instagram.Should().BeNullOrEmpty();
        }

        [Fact(DisplayName = "GetShareLinkByUserId should be null when request is null or empty")]
        public void GetShareLinkByUserIdShouldBeReturnsNull()
        {
            int? userId = null;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<int>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<int>()))).Returns(new Contact { });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration { });

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            Action act = () => userService.GetShareLinkByLoggedUser(userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "GetShareProfile should be returns ShareProfileDto based on userId")]
        public void GetShareProfileShouldBeReturnsShareLinkDto()
        {
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<long>()))).Returns(new Contact
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

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            var result = userService.GetShareProfile(3);

            result.Twitter.Should().BeEquivalentTo("https://twitter.com/TwitterUserName");
            result.Facebook.Should().BeEquivalentTo("https://www.facebook.com/FacebookUserName");
            result.Instagram.Should().BeEquivalentTo("https://www.instagram.com/InstagramUserName");
        }

        [Fact(DisplayName = "GetShareProfile should be null when request is null or empty")]
        public void GetShareProfileShouldBeReturnsNull()
        {
            long? userId = null;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(new User { });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<long>()))).Returns(new Contact { });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration { });

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            Action act = () => userService.GetShareProfile(userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "GetLoggedUserInfoById should be returns UserDto by userId")]
        public void GetLoggedUserInfoByIdShouldBeUserInfo()
        {
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(new User
            {
                UserID = 3,
                Username = "Username"
            });
            mockAddressRepository.Setup(x => x.GetAddressByUser((It.IsAny<long>()))).Returns(new Address
            {
                UserID = 3,
                Street = "Street"
            });
            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<long>()))).Returns(new Contact
            {
                UserID = 3,
                MainPhone = "999987766"
            });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration
            {
                FacebookProfile = "https://www.facebook.com/",
                TwitterProfile = "https://twitter.com/",
                InstagramProfile = "https://www.instagram.com/"
            });

            var expectedUser = new UserDto
            {
                UserID = 3,
                Username = "Username",
                Street = "Street",
                MainPhone = "999987766",
            };

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            var contactProfile = userService?.GetShareProfile(3);
            var contactShareLink = userService?.GetShareLinkByLoggedUser(3);
            var result = userService.GetLoggedUserInfoById(3);

            result.UserID.Should().Be(expectedUser.UserID);
            result.Username.Should().BeEquivalentTo(expectedUser.Username);
            result.Street.Should().BeEquivalentTo(expectedUser.Street);
            result.MainPhone.Should().BeEquivalentTo(expectedUser.MainPhone);
        }

        [Fact(DisplayName = "GetLoggedUserInfoById should be null when request is null or empty")]
        public void GetLoggedUserInfoByIdShouldBeReturnsNull()
        {
            int? userId = null;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<int>()))).Returns(new User { });
            mockAddressRepository.Setup(x => x.GetAddressByUser((It.IsAny<int>()))).Returns(new Address { });
            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<int>()))).Returns(new Contact { });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration { });

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            Action act = () => userService.GetLoggedUserInfoById(userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "GetShareProfile should be returns user info by userId")]
        public void GetUserInfoByIdShouldBeUserInfo()
        {
            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(new User
            {
                UserID = 3,
                Username = "Username"
            });

            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<long>()))).Returns(new Contact
            {
                UserID = 3,
                MainPhone = "Username"
            });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration
            {
                Facebook = "https://www.facebook.com/sharer/sharer.php?u=",
                Twitter = "https://twitter.com/intent/tweet?text=",
                Whatsapp = "https://wa.me/",
                ArtMais = "https://artmais-frontend.herokuapp.com/artista/"
            }
            );

            var expectedUser = new UserDto
            {
                UserID = 3,
                Username = "Username"
            };

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            var result = userService.GetUserInfoById(3);

            result.UserID.Should().Be(expectedUser.UserID);
            result.Username.Should().BeEquivalentTo(expectedUser.Username);
        }

        [Fact(DisplayName = "GetShareProfile should be null when request is null or empty")]
        public void GetUserInfoByIdShouldBeReturnsNull()
        {
            int? userId = null;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<int>()))).Returns(new User { });

            mockOptions.Setup(x => x.Value).Returns(new SocialMediaConfiguration { });

            var userService = new UserService(mockAddressRepository.Object, mockContactRepository.Object, mockOptions.Object, mockUserRepository.Object, mockMapper.Object);

            Action act = () => userService.GetUserInfoById(userId);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "UpdateUserInfo should be returns UserProfileInfoDto and update database")]
        public void UpdateUserInfoShouldBeUserInfo()
        {
            #region Mocks
            var userRequest = new UserRequest
            {
                Username = "Username",
                Name = "Name",
                MainPhone = "165987766",
                SecundaryPhone = "999987766"
            };

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userInfo = new User
            {
                UserID = 3,
                Username = "oldUsername",
                Name = "oldName"
            };

            var updatedUser = new User
            {
                UserID = 3,
                Username = "Username",
                Name = "Name"
            };

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(userInfo);
            mockMapper.Setup(m => m.Map(userRequest, userInfo)).Returns(updatedUser);
            mockUserRepository.Setup(x => x.Update((It.IsAny<User>()))).Returns(updatedUser);

            var contactInfo = new Contact
            {
                UserID = 3,
                MainPhone = "999987766"
            };
            var updatedContact = new Contact
            {
                UserID = 3,
                MainPhone = "165987766",
                SecundaryPhone = "999987766"
            };
            mockContactRepository.Setup(x => x.GetContactByUser((It.IsAny<long>()))).Returns(contactInfo);
            mockMapper.Setup(m => m.Map(userRequest, contactInfo)).Returns(updatedContact);
            mockContactRepository.Setup(x => x.Update((It.IsAny<Contact>()))).Returns(updatedContact);
            #endregion

            var userService = new UserService(
                mockAddressRepository.Object,
                mockContactRepository.Object,
                mockOptions.Object,
                mockUserRepository.Object,
                mockMapper.Object);

            var result = userService.UpdateUserInfo(userRequest, 3);

            result.UserId.Should().Be(updatedUser.UserID);
            result.Username.Should().BeEquivalentTo(updatedUser.Username);
            result.Name.Should().BeEquivalentTo(updatedUser.Name);
            result.MainPhone.Should().BeEquivalentTo(updatedContact.MainPhone);
            result.SecundaryPhone.Should().BeEquivalentTo(updatedContact.SecundaryPhone);
        }

        [Fact(DisplayName = "UpdateUserInfo should be null when request is null or empty")]
        public void UpdateUserInfoShouldBeReturnsNull()
        {
            UserRequest userRequest = null;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(
                mockAddressRepository.Object,
                mockContactRepository.Object,
                mockOptions.Object,
                mockUserRepository.Object,
                mockMapper.Object);

            Action act = () => userService.UpdateUserInfo(userRequest, 3);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "UpdateUserPassword should be true and update database")]
        public void UpdateUserPasswordShouldBeTrue()
        {
            #region Mocks
            var passwordRequest = new PasswordRequest
            {
                OldPassword = "padawans#2021",
                NewPassword = "Password",
                Password = "Password"
            };

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userInfo = new User
            {
                UserID = 3,
                Password = "l0zsJQtRbSsD8tNJ0gkX6Q==dyp1UJj1ErRfA9GuSpJyvOny80+g3lkuO5SEACWmXXsM72BWOFCXMYLSiXQLSb/CLaTMSUMk/1rQJXrLGQryQg=="
            };

            var updatedUser = new User
            {
                UserID = 3,
                Password = "k4xsqPZJOUVsO4PUZLOoKQ==ju7lMkPE3CHc97DTXw54lN1EG5mrtNw6OpLdsPS/sZ4b7XFYl5AnI3+ZdVrxQJmZF4J0HuNoyMP3cbDcxN04Kg=="
            };

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<long>()))).Returns(userInfo);
            mockMapper.Setup(m => m.Map(passwordRequest, userInfo)).Returns(updatedUser);
            mockUserRepository.Setup(x => x.Update((It.IsAny<User>()))).Returns(updatedUser);
            #endregion

            var userService = new UserService(
                mockAddressRepository.Object,
                mockContactRepository.Object,
                mockOptions.Object,
                mockUserRepository.Object,
                mockMapper.Object);

            var result = userService.UpdateUserPassword(passwordRequest, 3);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "UpdateUserPassword should be false when request is null or empty")]
        public void UpdateUserPasswordShouldBeFalse()
        {
            PasswordRequest passwordRequest = null;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(
                mockAddressRepository.Object,
                mockContactRepository.Object,
                mockOptions.Object,
                mockUserRepository.Object,
                mockMapper.Object);

            Action act = () => userService.UpdateUserPassword(passwordRequest, 3);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact(DisplayName = "UpdateUserDescription should be true and update database")]
        public void UpdateUserDescriptionShouldBeTrue()
        {
            #region Mocks
            var descriptionRequest = new DescriptionRequest
            {
                Description = "Description"
            };

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userInfo = new User
            {
                UserID = 3,
                Description = "oldDescription",
            };

            var updatedUser = new User
            {
                UserID = 3,
                Description = "Description",
            };

            mockUserRepository.Setup(x => x.GetUserById((It.IsAny<int>()))).Returns(userInfo);
            mockMapper.Setup(m => m.Map(descriptionRequest, userInfo)).Returns(updatedUser);
            mockUserRepository.Setup(x => x.Update((It.IsAny<User>()))).Returns(updatedUser);
            #endregion

            var userService = new UserService(
                mockAddressRepository.Object,
                mockContactRepository.Object,
                mockOptions.Object,
                mockUserRepository.Object,
                mockMapper.Object);

            var result = userService.UpdateUserDescription(descriptionRequest, 3);

            result.Should().BeTrue();
        }

        [Fact(DisplayName = "UpdateUserDescription should be false when request is null or empty")]
        public void UpdateUserDescriptionShouldBeFalse()
        {
            DescriptionRequest descriptionRequest = null;

            var mockAddressRepository = new Mock<IAddressRepository>();
            var mockContactRepository = new Mock<IContactRepository>();
            var mockOptions = new Mock<IOptions<SocialMediaConfiguration>>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockMapper = new Mock<IMapper>();

            var userService = new UserService(
                mockAddressRepository.Object,
                mockContactRepository.Object,
                mockOptions.Object,
                mockUserRepository.Object,
                mockMapper.Object);

            Action act = () => userService.UpdateUserDescription(descriptionRequest, 3);
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null.");
        }
    }
}
