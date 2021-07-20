using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Infrastructure.Options;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.Extensions.Options;
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

        public async Task<ShareLinkDto> GetShareLinkAsync(int userId)
        {
            var contact = await this._contactRepository.GetContactByUserAsync(userId);
            var shareLinkDto = new ShareLinkDto
            {
                Facebook = $"{_socialMediaConfiguration.Facebook}/dsadhsaudha{contact.Facebook}",
                Twitter = $"{_socialMediaConfiguration.Twitter}/dsadhsaudha{contact.Twitter}",
                MainPhone = $"{_socialMediaConfiguration.Whatsapp}/dsadhsaudha{contact.MainPhone}",
                SecundaryPhone = $"{_socialMediaConfiguration.Whatsapp}/dsadhsaudha{contact.SecundaryPhone}",
                ThirdPhone = $"{_socialMediaConfiguration.Whatsapp}/dsadhsaudha{contact.ThirdPhone}"
            };

            return shareLinkDto;
        }
    }
}