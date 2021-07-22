using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Users.Dto;
using AutoMapper;

namespace ArtmaisBackend.Infrastructure.Profiles
{
    public class UserMappingConfig : Profile
    {
        public UserMappingConfig()
        {
            CreateMap<User, UserDto>();
        }
    }
}
