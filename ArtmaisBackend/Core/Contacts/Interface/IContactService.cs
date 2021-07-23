using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Request;

namespace ArtmaisBackend.Core.Contacts.Interface
{
    public interface IContactService
    {
        ContactDto? Create(ContactRequest contactRequest, int userId);
        ContactDto? GetContactByUser(int userId);
    }
}
