using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Users.Dto;
using AutoMapper;

namespace ArtmaisBackend.Infrastructure.Profiles
{
    public class UserMappingConfig : Profile
    {
        public UserMappingConfig()
        {
            this.CreateMap<User, UserDto>();
            this.CreateMap<Contact, ContactDto>();
        }
    }
}
