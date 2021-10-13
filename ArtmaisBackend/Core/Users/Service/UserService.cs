using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Core.Users.Request;
using ArtmaisBackend.Infrastructure;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;

namespace ArtmaisBackend.Core.Users.Service
{
    public class UserService : IUserService
    {
        public UserService(IAddressRepository addressRepository, IContactRepository contactRepository, IOptions<SocialMediaConfiguration> options, IUserRepository userRepository, IMapper mapper)
        {
            this._addressRepository = addressRepository;
            this._contactRepository = contactRepository;
            this._socialMediaConfiguration = options.Value;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        private readonly IAddressRepository _addressRepository;
        private readonly IContactRepository _contactRepository;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ShareLinkDto? GetShareLinkByLoggedUser(long? userId)
        {
            var user = this._userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var shareLinkDto = new ShareLinkDto
            {
                Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}",
                Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}",
                Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}"
            };
            return shareLinkDto;
        }

        public ShareLinkDto? GetShareLinkByUserId(long? userId)
        {
            var user = this._userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var contact = this._contactRepository.GetContactByUser(user.UserID);

            var shareLinkDto = new ShareLinkDto
            {
                Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                WhatsappContact = $"{this._socialMediaConfiguration.Whatsapp}?phone={contact?.MainPhone}&text={ShareLinkMessages.MessageComunication}"
            };

            return shareLinkDto;
        }

        public ShareProfileBaseDto? GetShareProfile(long? userId)
        {
            var user = this._userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var contact = this._contactRepository.GetContactByUser(user.UserID);

            if (contact is null)
            {
                return new ShareProfileBaseDto { };
            }
            else
            {
                var shareProfileDto = new ShareProfileBaseDto
                {
                    Facebook = $"{this._socialMediaConfiguration.FacebookProfile }{contact?.Facebook}",
                    Twitter = $"{this._socialMediaConfiguration.TwitterProfile }{contact?.Twitter}",
                    Instagram = $"{this._socialMediaConfiguration.InstagramProfile }{contact?.Instagram}",
                };

                return shareProfileDto;
            }
        }

        public UserDto? GetLoggedUserInfoById(long? userId)
        {
            var user = this._userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var userCategory = this._userRepository.GetSubcategoryByUserId(user.UserID);
            var contact = this._contactRepository.GetContactByUser(user.UserID);
            var address = this._addressRepository.GetAddressByUser(user.UserID);
            var contactProfile = this.GetShareProfile(user.UserID);
            var contactShareLink = this.GetShareLinkByLoggedUser(user.UserID);

            var userDto = new UserDto()
            {
                UserID = user.UserID,
                Name = user?.Name,
                Username = user?.Username,
                BirthDate = user?.BirthDate,
                UserPicture = user?.UserPicture,
                BackgroundPicture = user?.BackgroundPicture,
                Category = userCategory?.Category,
                Subcategory = userCategory?.Subcategory,
                Description = user?.Description,
                Street = address?.Street,
                Number = address?.Number,
                City = address?.City,
                State = address?.State,
                Neighborhood = address?.Neighborhood,
                ZipCode = address?.ZipCode,
                UserTwitter = contact?.Twitter,
                UserInstagram = contact?.Instagram,
                UserFacebook = contact?.Facebook,
                Facebook = contactProfile?.Facebook,
                Twitter = contactProfile?.Twitter,
                Instagram = contactProfile?.Instagram,
                MainPhone = contact?.MainPhone,
                SecundaryPhone = contact?.SecundaryPhone,
                ThirdPhone = contact?.ThirdPhone,
                FacebookProfile = contactShareLink?.Facebook,
                TwitterProfile = contactShareLink?.Twitter,
                WhatsappProfile = contactShareLink?.Whatsapp
            };

            return userDto;
        }

        public UserDto? GetUserInfoById(long? userId)
        {
            var user = this._userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var userCategory = this._userRepository.GetSubcategoryByUserId(user.UserID);
            var contact = this._contactRepository.GetContactByUser(user.UserID);
            var address = this._addressRepository.GetAddressByUser(user.UserID);
            var contactProfile = this.GetShareProfile(user.UserID);
            var contactShareLink = this.GetShareLinkByUserId(user.UserID);

            var userDto = new UserDto()
            {
                UserID = user.UserID,
                Name = user?.Name,
                Username = user?.Username,
                BirthDate = user?.BirthDate,
                UserPicture = user?.UserPicture,
                BackgroundPicture = user?.BackgroundPicture,
                Category = userCategory?.Category,
                Subcategory = userCategory?.Subcategory,
                Description = user?.Description,
                Street = address?.Street,
                Number = address?.Number,
                City = address?.City,
                State = address?.State,
                Neighborhood = address?.Neighborhood,
                ZipCode = address?.ZipCode,
                Facebook = contactProfile?.Facebook,
                Twitter = contactProfile?.Twitter,
                Instagram = contactProfile?.Instagram,
                MainPhone = contact?.MainPhone,
                SecundaryPhone = contact?.SecundaryPhone,
                ThirdPhone = contact?.ThirdPhone,
                FacebookProfile = contactShareLink?.Facebook,
                TwitterProfile = contactShareLink?.Twitter,
                WhatsappProfile = contactShareLink?.Whatsapp,
                WhatsappContact = contactShareLink?.WhatsappContact
            };

            return userDto;
        }

        public UserProfileInfoDto UpdateUserInfo(UserRequest? userRequest, long userId)
        {
            if (userRequest is null)
                throw new ArgumentNullException();

            var userInfo = this._userRepository.GetUserById(userId);
            userInfo = this._mapper.Map(userRequest, userInfo);
            var user = this._userRepository.Update(userInfo);

            var userContactInfo = this._contactRepository.GetContactByUser(user.UserID);

            if (userContactInfo is null)
            {
                var contactRequest = this._mapper.Map<ContactRequest>(userRequest);
                var newContact = this._contactRepository.Create(contactRequest, user.UserID);
                if (newContact is null)
                    throw new ArgumentNullException();

                var userDto = new UserProfileInfoDto
                {
                    UserId = user.UserID,
                    Username = user?.Username,
                };

                return userDto;
            }
            else
            {
                userContactInfo = this._mapper.Map(userRequest, userContactInfo);
                this._contactRepository.Update(userContactInfo);

                var userDto = new UserProfileInfoDto
                {
                    UserId = user.UserID,
                    Username = user?.Username,
                };

                return userDto;
            }
        }

        public bool UpdateUserPassword(PasswordRequest? passwordRequest, long userId)
        {
            if (passwordRequest is null || passwordRequest.NewPassword == "" || passwordRequest.Password == "")
                throw new ArgumentNullException();

            if (!(passwordRequest.Password.Equals(passwordRequest.NewPassword)))
                throw new ArgumentException();

            var userInfo = this._userRepository.GetUserById(userId);
            if (userInfo is null)
                throw new ArgumentNullException();

            var salt = userInfo.Password.Substring(0, 24);
            var encryptedPassword = PasswordUtil.Encrypt(passwordRequest.OldPassword, salt);

            if (!encryptedPassword.Equals(userInfo.Password))
                throw new AccessViolationException();

            passwordRequest.Password = PasswordUtil.Encrypt(passwordRequest.Password);

            this._mapper.Map(passwordRequest, userInfo);
            this._userRepository.Update(userInfo);

            return true;
        }

        public bool UpdateUserDescription(UserDescriptionRequest? userDescriptionRequest, long userId)
        {
            if (userDescriptionRequest is null)
                throw new ArgumentNullException();

            var userInfo = this._userRepository.GetUserById(userId);

            this._mapper.Map(userDescriptionRequest, userInfo);
            this._userRepository.Update(userInfo);

            return true;
        }
    }
}