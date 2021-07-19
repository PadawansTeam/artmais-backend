using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class ContactRepository : IContactRepository
    {
        public ContactRepository(ArtplusContext context)
        {
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public async Task<Contact?> GetContactByUserAsync(int userId)
        {
            return await this._context.Contact.FirstOrDefaultAsync(contact => contact.User.UserID == userId);
        }
    }
}
