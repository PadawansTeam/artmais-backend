using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Interface;
using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using AutoMapper;
using System;

namespace ArtmaisBackend.Core.Contact.Service
{
    public class ContactService : IContactService
    {
        public ContactService(IContactRepository contactRepository, IMapper mapper)
        {
            _contactRepository = contactRepository;
            _mapper = mapper;
        }

        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactDto? CreateOrUpdateUserContact(ContactRequest? contactRequest, long userId)
        {
            if (contactRequest is null)
            {
                throw new ArgumentNullException();
            }

            var contactInfo = _contactRepository.GetContactByUser(userId);

            if (contactInfo is null)
            {
                var newContact = _contactRepository.Create(contactRequest, userId);
                if (newContact is null)
                {
                    throw new ArgumentNullException();
                }

                var contactDto = _mapper.Map<ContactDto>(newContact);

                return contactDto;
            }
            else
            {
                _mapper.Map(contactRequest, contactInfo);
                var contact = _contactRepository.Update(contactInfo);

                var contactDto = _mapper.Map<ContactDto>(contact);

                return contactDto;
            }
        }

        public ContactDto? GetContactByUser(long userId)
        {
            var contact = _contactRepository.GetContactByUser(userId);
            if (contact is null)
            {
                throw new ArgumentNullException();
            }

            var contactDto = _mapper.Map<ContactDto>(contact);
            return contactDto;
        }
    }
}
