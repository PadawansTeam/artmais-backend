using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Core.Users.Request;
using ArtmaisBackend.Infrastructure;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util;
using AutoMapper;
using Microsoft.Extensions.Options;

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

        public ShareLinkDto? GetShareLinkByLoggedUser(int? userId)
        {
            var user = this._userRepository.GetUserById(userId);
            if (user is null) return null;

            var contact = this._contactRepository.GetContactByUser(user.UserID);
            if (contact is null) return null;

            var shareLinkDto = new ShareLinkDto
            {
                Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}",
                Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}",
                Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareProfile}"
            };
            return shareLinkDto;
        }

        public ShareLinkDto? GetShareLinkByUserId(int? userId)
        {
            var user = this._userRepository.GetUserById(userId);
            if (user is null) return null;

            var contact = this._contactRepository.GetContactByUser(user.UserID);
            if (contact is null) return null;

            var shareLinkDto = new ShareLinkDto
            {
                Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{user.UserID}{ShareLinkMessages.MessageShareLink}",
                WhatsappContact = $"{this._socialMediaConfiguration.Whatsapp}?phone={contact?.MainPhone}&text={ShareLinkMessages.MessageComunication}"
            };

            return shareLinkDto;
        }

        public ShareProfileBaseDto? GetShareProfile(int? userId)
        {
            var user = this._userRepository.GetUserById(userId);

            if (user is null) return null;

            var contact = this._contactRepository.GetContactByUser(user.UserID);

            if (contact is null) return null;

            var shareProfileDto = new ShareProfileBaseDto
            {
                Facebook = $"{this._socialMediaConfiguration.FacebookProfile }{contact?.Facebook}",
                Twitter = $"{this._socialMediaConfiguration.TwitterProfile }{contact?.Twitter}",
                Instagram = $"{this._socialMediaConfiguration.InstagramProfile }{contact?.Instagram}",
            };

            return shareProfileDto;
        }

        public UserDto? GetLoggedUserInfoById(int? id)
        {
            var user = this._userRepository.GetUserById(id);
            if (user is null) return null;

            var userCategory = this._userRepository.GetSubcategoryByUserId(user.UserID);
            var contact = this._contactRepository.GetContactByUser(id);
            var address = this._addressRepository.GetAddressByUser(id);
            var contactProfile = this.GetShareProfile(id);
            var contactShareLink = GetShareLinkByLoggedUser(id);

            var userDto = new UserDto()
            {
                UserID = user.UserID,
                Name = user?.Name,
                Username = user?.Username,
                UserPicture = user?.UserPicture,
                BackgroundPicture = user?.BackgroundPicture,
                Category = userCategory?.Category,
                Subcategory = userCategory?.Subcategory,
                Description = user?.Description,
                Street = address?.Street,
                Number = address?.Number,
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

        public UserDto? GetUserInfoById(int? id)
        {
            var user = this._userRepository.GetUserById(id);
            if (user is null) return null;

            var userCategory = this._userRepository.GetSubcategoryByUserId(user.UserID);
            var contact = this._contactRepository.GetContactByUser(id);
            var address = this._addressRepository.GetAddressByUser(id);
            var contactProfile = this.GetShareProfile(id);
            var contactShareLink = GetShareLinkByUserId(id);

            var userDto = new UserDto()
            {
                UserID = user.UserID,
                Name = user?.Name,
                Username = user?.Username,
                UserPicture = user?.UserPicture,
                BackgroundPicture = user?.BackgroundPicture,
                Category = userCategory?.Category,
                Subcategory = userCategory?.Subcategory,
                Description = user?.Description,
                Street = address?.Street,
                Number = address?.Number,
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

        public UserProfileInfoDto UpdateUserInfo(UserRequest? userRequest, int userId)
        {
            if (userRequest is null) return null;

            var userInfo = this._userRepository.GetUserById(userId);
            userInfo = this._mapper.Map(userRequest, userInfo);
            var user = this._userRepository.Update(userInfo);

            var userContactInfo = this._contactRepository.GetContactByUser(userId);
            userContactInfo = this._mapper.Map(userRequest, userContactInfo);
            var contact = this._contactRepository.Update(userContactInfo);

            var userDto = new UserProfileInfoDto { 
                UserId = user.UserID,
                Name = user.Name,
                Username = user.Username,
                UserPicture = user.UserPicture,
                BirthDate = user.BirthDate,
                MainPhone = contact.MainPhone,
                SecundaryPhone = contact.SecundaryPhone,
                ThirdPhone = contact.ThirdPhone
            };

            return userDto;
        }

        public bool UpdateUserPassword(PasswordRequest? passwordRequest, int userId)
        {
            if (passwordRequest is null || passwordRequest.NewPassword == "" || passwordRequest.Password == "") return false;
            if (!(passwordRequest.Password.Equals(passwordRequest.NewPassword))) return false;

            var userInfo = this._userRepository.GetUserById(userId);

            var salt = userInfo.Password.Substring(0, 24);
            var encryptedPassword = PasswordUtil.Encrypt(passwordRequest.OldPassword, salt);

            if(!encryptedPassword.Equals(userInfo.Password)) return false;

            passwordRequest.Password = PasswordUtil.Encrypt(passwordRequest.Password);
           
            this._mapper.Map(passwordRequest, userInfo);
            this._userRepository.Update(userInfo);

            return true;
        }

        public bool UpdateUserDescription(DescriptionRequest? descriptionRequest, int userId)
        {
            if (descriptionRequest is null) return false;

            var userInfo = this._userRepository.GetUserById(userId);

            this._mapper.Map(descriptionRequest, userInfo);
            this._userRepository.Update(userInfo);

            return true;
        }
    }
}