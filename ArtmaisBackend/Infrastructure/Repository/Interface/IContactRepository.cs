using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.Entities;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IContactRepository
    {
        Contact? GetContactByUser(int userId);
        Contact Create(ContactRequest contactRequest, int userId);
    }
}
