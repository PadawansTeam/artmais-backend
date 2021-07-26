using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class ContactRepository : IContactRepository
    {
        public ContactRepository(ArtplusContext context)
        {
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public Contact? GetContactByUser(long? userId)
        {
            return this._context.Contact.FirstOrDefault(contact => contact.UserID == userId);
        }

        public Contact Create(ContactRequest contactRequest, long userId)
        {
            var contact = new Contact
            {
                UserID = userId,
                Facebook = contactRequest.Facebook,
                Instagram = contactRequest.Instagram,
                Twitter = contactRequest.Twitter,
                MainPhone = contactRequest.MainPhone,
                SecundaryPhone = contactRequest.SecundaryPhone,
                ThirdPhone = contactRequest.ThirdPhone,
            };

            this._context.Contact.Add(contact);
            this._context.SaveChanges();

            return contact;
        }
        public Contact Update(Contact contact)
        {
            this._context.Contact.Update(contact);
            this._context.SaveChanges();

            return contact;
        }
    }
}