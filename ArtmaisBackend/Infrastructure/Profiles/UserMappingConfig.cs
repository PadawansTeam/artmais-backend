using ArtmaisBackend.Core.Adresses.Dto;
using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Core.Users.Request;
using AutoMapper;

namespace ArtmaisBackend.Infrastructure.Profiles
{
    public class UserMappingConfig : Profile
    {
        public UserMappingConfig()
        {
            this.CreateMap<User, UserDto>();
            this.CreateMap<UserRequest, User>();
            this.CreateMap<PasswordRequest, User>();
            this.CreateMap<UserRequest, Contact>();
            this.CreateMap<Contact, ContactDto>();
            this.CreateMap<ContactRequest, Contact>();
            this.CreateMap<Address, AddressDto>();
            this.CreateMap<AddressRequest, Address>();
        }
    }
}
