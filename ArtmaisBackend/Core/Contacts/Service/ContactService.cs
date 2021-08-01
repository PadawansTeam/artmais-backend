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
            this._contactRepository = contactRepository;
            this._mapper = mapper;
        }

        private readonly IContactRepository _contactRepository;
        private readonly IMapper _mapper;

        public ContactDto? CreateOrUpdateUserContact(ContactRequest? contactRequest, int userId)
        {
            if (contactRequest is null)
                throw new ArgumentNullException();
            
            var contactInfo = this._contactRepository.GetContactByUser(userId);

            if(contactInfo is null)
            {
                var newContact = this._contactRepository.Create(contactRequest, userId);
                if (newContact is null)
                    throw new ArgumentNullException();

                var contactDto = this._mapper.Map<ContactDto>(newContact);

                return contactDto;
            }
            else
            {
                this._mapper.Map(contactRequest, contactInfo);
                var contact = this._contactRepository.Update(contactInfo);

                var contactDto = this._mapper.Map<ContactDto>(contact);

                return contactDto;
            }
        }

        public ContactDto? GetContactByUser(int userId)
        {
            var contact = this._contactRepository.GetContactByUser(userId);
            if (contact is null)
                throw new ArgumentNullException();

            var contactDto = this._mapper.Map<ContactDto>(contact);
            return contactDto;
        }
    }
}
