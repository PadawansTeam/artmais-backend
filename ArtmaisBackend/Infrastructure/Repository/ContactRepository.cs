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
    }
}
