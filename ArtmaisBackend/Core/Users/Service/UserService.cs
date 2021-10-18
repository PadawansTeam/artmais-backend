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
            _addressRepository = addressRepository;
            _contactRepository = contactRepository;
            _socialMediaConfiguration = options.Value;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        private readonly IAddressRepository _addressRepository;
        private readonly IContactRepository _contactRepository;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ShareLinkDto? GetShareLinkByLoggedUser(long? userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var shareLinkDto = new ShareLinkDto
            {
                Facebook = $"{_socialMediaConfiguration.Facebook}{_socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}",
                Twitter = $"{_socialMediaConfiguration.Twitter}{_socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}",
                Whatsapp = $"{_socialMediaConfiguration.Whatsapp}?text={_socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}"
            };
            return shareLinkDto;
        }

        public ShareLinkDto? GetShareLinkByUserId(long? userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var contact = _contactRepository.GetContactByUser(user.UserID);

            var shareLinkDto = new ShareLinkDto
            {
                Facebook = $"{_socialMediaConfiguration.Facebook}{_socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                Twitter = $"{_socialMediaConfiguration.Twitter}{_socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                Whatsapp = $"{_socialMediaConfiguration.Whatsapp}?text={_socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                WhatsappContact = $"{_socialMediaConfiguration.Whatsapp}?phone={contact?.MainPhone}&text={ShareLinkMessages.MessageComunication}"
            };

            return shareLinkDto;
        }

        public ShareProfileBaseDto? GetShareProfile(long? userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var contact = _contactRepository.GetContactByUser(user.UserID);

            if (contact is null)
            {
                return new ShareProfileBaseDto { };
            }
            else
            {
                var shareProfileDto = new ShareProfileBaseDto
                {
                    Facebook = $"{_socialMediaConfiguration.FacebookProfile }{contact?.Facebook}",
                    Twitter = $"{_socialMediaConfiguration.TwitterProfile }{contact?.Twitter}",
                    Instagram = $"{_socialMediaConfiguration.InstagramProfile }{contact?.Instagram}",
                };

                return shareProfileDto;
            }
        }

        public UserDto? GetLoggedUserInfoById(long? userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var userCategory = _userRepository.GetSubcategoryByUserId(user.UserID);
            var contact = _contactRepository.GetContactByUser(user.UserID);
            var address = _addressRepository.GetAddressByUser(user.UserID);
            var contactProfile = GetShareProfile(user.UserID);
            var contactShareLink = GetShareLinkByLoggedUser(user.UserID);

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
            var user = _userRepository.GetUserById(userId);
            if (user is null)
                throw new ArgumentNullException();

            var userCategory = _userRepository.GetSubcategoryByUserId(user.UserID);
            var contact = _contactRepository.GetContactByUser(user.UserID);
            var address = _addressRepository.GetAddressByUser(user.UserID);
            var contactProfile = GetShareProfile(user.UserID);
            var contactShareLink = GetShareLinkByUserId(user.UserID);

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

            var userInfo = _userRepository.GetUserById(userId);
            userInfo = _mapper.Map(userRequest, userInfo);
            var user = _userRepository.Update(userInfo);

            var userContactInfo = _contactRepository.GetContactByUser(user.UserID);

            if (userContactInfo is null)
            {
                var contactRequest = _mapper.Map<ContactRequest>(userRequest);
                var newContact = _contactRepository.Create(contactRequest, user.UserID);
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
                userContactInfo = _mapper.Map(userRequest, userContactInfo);
                _contactRepository.Update(userContactInfo);

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

            var userInfo = _userRepository.GetUserById(userId);
            if (userInfo is null)
                throw new ArgumentNullException();

            var salt = userInfo.Password.Substring(0, 24);
            var encryptedPassword = PasswordUtil.Encrypt(passwordRequest.OldPassword, salt);

            if (!encryptedPassword.Equals(userInfo.Password))
                throw new AccessViolationException();

            passwordRequest.Password = PasswordUtil.Encrypt(passwordRequest.Password);

            _mapper.Map(passwordRequest, userInfo);
            _userRepository.Update(userInfo);

            return true;
        }

        public bool UpdateUserDescription(UserDescriptionRequest? userDescriptionRequest, long userId)
        {
            if (userDescriptionRequest is null)
                throw new ArgumentNullException();

            var userInfo = _userRepository.GetUserById(userId);

            _mapper.Map(userDescriptionRequest, userInfo);
            _userRepository.Update(userInfo);

            return true;
        }
    }
}