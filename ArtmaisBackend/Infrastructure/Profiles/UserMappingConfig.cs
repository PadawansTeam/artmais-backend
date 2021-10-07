using ArtmaisBackend.Core.Adresses.Dto;
using ArtmaisBackend.Core.Adresses.Request;
using ArtmaisBackend.Core.Aws.Dto;
using ArtmaisBackend.Core.Contacts.Dto;
using ArtmaisBackend.Core.Contacts.Request;
using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Portfolio.Dto;
using ArtmaisBackend.Core.Portfolio.Request;
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
            this.CreateMap<UserDescriptionRequest, User>();
            this.CreateMap<AwsDto, User>()
                .ForMember(dest => dest.UserPicture, opt => opt.MapFrom(src => src.Content));
            this.CreateMap<UserRequest, Contact>();
            this.CreateMap<Contact, ContactDto>();
            this.CreateMap<PortfolioDescriptionRequest, Publication>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.PublicationDescription))
                .ForMember(dest => dest.PublicationID, opt => opt.MapFrom(src => src.PublicationId));
            this.CreateMap<PortfolioContentDto, Publication>();
            this.CreateMap<UserRequest, ContactRequest>();
            this.CreateMap<Contact, ContactRequest>();
            this.CreateMap<ContactRequest, Contact>();
            this.CreateMap<Address, AddressDto>();
            this.CreateMap<AddressRequest, Address>();
        }
    }
}
