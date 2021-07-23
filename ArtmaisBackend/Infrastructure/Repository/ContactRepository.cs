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

        public Contact? GetContactByUser(int userId)
        {
            return this._context.Contact.Where(contact => contact.UserID == userId).FirstOrDefault();
        }

        public Contact Create(ContactRequest contactRequest, int userId)
        {
            var contact = new Contact
            {
                UserID = userId,
                Facebook = contactRequest.Facebook,
                Instagram = contactRequest.Instagram,
                Twitter = contactRequest.Twitter,
                MainPhone = contactRequest.MainPhone,
                SecundaryPhone = contactRequest.Facebook,
                ThirdPhone = contactRequest.Facebook,
            };

            this._context.Contact.Add(contact);
            this._context.SaveChanges();

            return contact;
        }
    }
}