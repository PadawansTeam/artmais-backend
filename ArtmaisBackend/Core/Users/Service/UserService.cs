using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Infrastructure;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Users.Service
{
    public class UserService : IUserService
    {

        public UserService(IContactRepository contactRepository, IOptions<SocialMediaConfiguration> options)
        {
            this._contactRepository = contactRepository;
            this._socialMediaConfiguration = options.Value;
        }

        private readonly IContactRepository _contactRepository;
        private readonly SocialMediaConfiguration _socialMediaConfiguration;

        public async Task<ShareLinkDto> GetShareLinkAsync(int userId, string userName)
        {
            try
            {
                var contact = await this._contactRepository.GetContactByUserAsync(userId);
                var shareLinkDto = new ShareLinkDto
                {
                    Facebook = $"{this._socialMediaConfiguration.Facebook}{this._socialMediaConfiguration.ArtMais}{userName}{ShareLinkMessages.MessageShareLink}",
                    Twitter = $"{this._socialMediaConfiguration.Twitter}{this._socialMediaConfiguration.ArtMais}{userName}{ShareLinkMessages.MessageShareLink}",
                    Whatsapp = $"{this._socialMediaConfiguration.Whatsapp}{this._socialMediaConfiguration.ArtMais}{userName}{ShareLinkMessages.MessageShareLink}",
                    WhatsappContact = $"{this._socialMediaConfiguration.Whatsapp}?phone={contact?.MainPhone}{this._socialMediaConfiguration.ArtMais}/{userName}{ShareLinkMessages.MessageComunication}",
                    Instagram = ""
                };

                return shareLinkDto;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}