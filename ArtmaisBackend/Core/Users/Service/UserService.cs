using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Core.Users.Request;
using ArtmaisBackend.Infrastructure;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Options;
using System;

namespace ArtmaisBackend.Core.Users.Service
{
    public class UserService : IUserService
    {

        public UserService(IContactRepository contactRepository, IOptions<SocialMediaConfiguration> options, IUserRepository userRepository)
        {
            this._contactRepository = contactRepository;
            this._socialMediaConfiguration = options.Value;
            this._userRepository = userRepository;
        }

        private readonly IContactRepository _contactRepository;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;
        private readonly IUserRepository _userRepository;

        public ShareLinkDto GetShareLink(UsernameRequest usernameRequest, string userNamePerfil)
        {
            if (string.IsNullOrEmpty(usernameRequest.Username)) throw new ArgumentNullException();

            var user = this._userRepository.GetUserByUsername(usernameRequest.Username);
            if (user is null) throw new ArgumentNullException();

            var contact = this._contactRepository.GetContactByUser(user.UserID);
            if (contact is null) throw new ArgumentNullException();

            if (userNamePerfil.Equals(usernameRequest.Username))
            {
                var shareLinkDto = new ShareLinkDto
                {
                    Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{usernameRequest.Username}{ShareLinkMessages.MessageSharePerfil}",
                    Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{usernameRequest.Username}{ShareLinkMessages.MessageSharePerfil}",
                    Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}text={this._socialMediaConfiguration.ArtMais}{usernameRequest.Username}{ShareLinkMessages.MessageSharePerfil}"
                };
                return shareLinkDto;
            }
            else
            {
                var shareLinkDto = new ShareLinkDto
                {
                    Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{userNamePerfil}{ShareLinkMessages.MessageShareLink}",
                    Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{userNamePerfil}{ShareLinkMessages.MessageShareLink}",
                    Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}text={this._socialMediaConfiguration.ArtMais}{userNamePerfil}{ShareLinkMessages.MessageShareLink}",
                    WhatsappContact = $"{this._socialMediaConfiguration.Whatsapp}?phone={contact?.MainPhone}&text={ShareLinkMessages.MessageComunication}"
                };

                return shareLinkDto;
            }
        }

        public SharePerfilBaseDto GetShareProfile(UsernameRequest usernameRequest)
        {
            if (string.IsNullOrEmpty(usernameRequest.Username)) throw new ArgumentNullException();

            var user = this._userRepository.GetUserByUsername(usernameRequest.Username);

            if (user is null) throw new ArgumentNullException();

            var contact = this._contactRepository.GetContactByUser(user.UserID);

            if (contact is null) throw new ArgumentNullException();

            var shareProfileDto = new SharePerfilBaseDto
            {
                Facebook = $"{this._socialMediaConfiguration.FacebookPerfil}{contact?.Facebook}",
                Twitter = $"{this._socialMediaConfiguration.TwitterPerfil}{contact?.Twitter}",
                Instagram = $"{this._socialMediaConfiguration.InstagramPerfil}{contact?.Instagram}",
            };

            return shareProfileDto;
        }
    }
}