using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Infrastructure;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
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
                Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareProfile}",
                Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareProfile}",
                Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareProfile}"
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
                Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareLink}",
                Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareLink}",
                Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareLink}",
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
            
            var contact = this._contactRepository.GetContactByUser(id);
            if (contact is null) return null;

            var address = this._addressRepository.GetAddressByUser(id);
            if (address is null) return null;

            var contactProfile = this.GetShareProfile(id);
            var contactShareLink = GetShareLinkByLoggedUser(id);

            var userDto = new UserDto()
            {
                UserID = user.UserID,
                Name = user.Name,
                Username = user.Username,
                UserPicture = user.UserPicture,
                BackgroundPicture = user.BackgroundPicture,
                Street = address.Street,
                Number = address.Number,
                Neighborhood = address.Neighborhood,
                ZipCode = address.ZipCode,
                Facebook = contactProfile.Facebook,
                Twitter = contactProfile.Twitter,
                Instagram = contactProfile.Instagram,
                MainPhone = contact.MainPhone,
                SecundaryPhone = contact.SecundaryPhone,
                ThridPhone = contact.ThirdPhone,
                FacebookProfile = contactShareLink.Facebook,
                InstagramProfile = contactShareLink.Twitter,
                WhatsappProfile = contactShareLink.Whatsapp
            };

            return userDto;
        }

        public UserDto? GetUserInfoById(int? id)
        {
            var user = this._userRepository.GetUserById(id);
            if (user is null) return null;
            
            var contact = this._contactRepository.GetContactByUser(id);
            if (contact is null) return null;

            var address = this._addressRepository.GetAddressByUser(id);
            if (address is null) return null;

            var contactProfile = this.GetShareProfile(id);
            var contactShareLink = GetShareLinkByUserId(id);

            var userDto = new UserDto()
            {
                UserID = user.UserID,
                Name = user.Name,
                Username = user.Username,
                UserPicture = user.UserPicture,
                BackgroundPicture = user.BackgroundPicture,
                Street = address.Street,
                Number = address.Number,
                Neighborhood = address.Neighborhood,
                ZipCode = address.ZipCode,
                Facebook = contactProfile.Facebook,
                Twitter = contactProfile.Twitter,
                Instagram = contactProfile.Instagram,
                MainPhone = contact.MainPhone,
                SecundaryPhone = contact.SecundaryPhone,
                ThridPhone = contact.ThirdPhone,
                FacebookProfile = contactShareLink.Facebook,
                InstagramProfile = contactShareLink.Twitter,
                WhatsappProfile = contactShareLink.Whatsapp,
                WhatsappContact = contactShareLink.WhatsappContact
            };

            return userDto;
        }
    }
}