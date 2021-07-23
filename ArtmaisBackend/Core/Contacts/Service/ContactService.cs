using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;

namespace ArtmaisBackend.Core.Contact.Service
{
    public class ContactService
    {
        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            this._contactRepository = contactRepository;
            this._mapper = mapper;
        }

        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactDto? Create(ContactRequest contactRequest, int userId)
        {
            if (contactRequest is null) return null;

            var contact = this._contactRepository.Create(contactRequest, userId);
            if (contact is null) return null;

            var contactDto = this._mapper.Map<ContactDto>(contact);

            return contactDto;
        }
    }
}
