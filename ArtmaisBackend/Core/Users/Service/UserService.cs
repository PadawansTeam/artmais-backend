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

        public UserService(IContactRepository contactRepository, IOptions<SocialMediaConfiguration> options, IUserRepository userRepository, IMapper mapper)
        {
            this._contactRepository = contactRepository;
            this._socialMediaConfiguration = options.Value;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }

        private readonly IContactRepository _contactRepository;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ShareLinkDto? GetShareLink(int? userId, int userIdProfile)
        {
            var user = this._userRepository.GetUserById(userId);
            if (user is null) return null;

            var userProfile = this._userRepository.GetUserById(userIdProfile);
            if (userProfile is null) return null;

            var contact = this._contactRepository.GetContactByUser(user.UserID);
            if (contact is null) return null;

            if (userIdProfile.Equals(userId))
            {
                var shareLinkDto = new ShareLinkDto
                {
                    Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{userProfile.Username}{ShareLinkMessages.MessageShareProfile}",
                    Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{userProfile.Username}{ShareLinkMessages.MessageShareProfile}",
                    Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{userProfile.Username}{ShareLinkMessages.MessageShareProfile}"
                };
                return shareLinkDto;
            }
            else
            {
                var shareLinkDto = new ShareLinkDto
                {
                    Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareLink}",
                    Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareLink}",
                    Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}?text={this._socialMediaConfiguration.ArtMais}{user.Username}{ShareLinkMessages.MessageShareLink}",
                    WhatsappContact = $"{this._socialMediaConfiguration.Whatsapp}?phone={contact?.MainPhone}&text={ShareLinkMessages.MessageComunication}"
                };

                return shareLinkDto;
            }
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

        public UserDto? GetUserInfoById(int? id)
        {
            var user = this._userRepository.GetUserById(id);
            if (user is null) return null;

            var userDto = this._mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}