using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Request;

namespace ArtmaisBackend.Core.Contacts.Interface
{
    public interface IContactService
    {
        ContactDto? CreateOrUpdateUserAddress(ContactRequest? contactRequest, int userId);
        ContactDto? GetContactByUser(int userId);
    }
}
