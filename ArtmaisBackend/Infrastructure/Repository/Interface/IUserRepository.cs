using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.SignUp;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        User Create(SignUpRequest signUpRequest);
        User GetUserByEmail(string email);
        IEnumerable<RecomendationDto> GetUsersByInterest(int userId);
        User GetUserByUsername(string username);
    }
}
