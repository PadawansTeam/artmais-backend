using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Interface;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Users.Service
{
    public class UserService : IUserService
    {

        public UserService(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }

        private readonly IContactRepository _contactRepository;

        public async Task<ShareLinkDto> GetShareLinkAsync(int userId)
        {
            var contact = await this._contactRepository.GetContactByUserAsync(userId);
            var shareLinkDto = new ShareLinkDto
            {
                Facebook = contact?.Facebook,
                Instagram = contact?.Instagram,
                Twitter = $"https:\\twitter.com\\intent\\tweet?text=Hello%20world{contact?.Twitter}",
                MainPhone = contact?.MainPhone,
                SecundaryPhone = contact?.SecundaryPhone,
                ThirdPhone = contact?.ThirdPhone
            };

            return shareLinkDto;
        }
    }
}