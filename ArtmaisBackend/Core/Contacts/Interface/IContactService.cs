using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Request;

namespace ArtmaisBackend.Core.Contacts.Interface
{
    public interface IContactService
    {
        ContactDto? CreateOrUpdateUserContact(ContactRequest? contactRequest, long userId);
        ContactDto? GetContactByUser(long userId);
    }
}
