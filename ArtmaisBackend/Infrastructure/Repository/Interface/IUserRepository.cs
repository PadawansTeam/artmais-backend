using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.SignUp.Request;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        User Create(SignUpRequest signUpRequest);
        User GetUserByEmail(string email);
        IEnumerable<RecomendationDto> GetUsersByInterest(int userId);
        User GetUserByUsername(string username);
        User GetUserById(int? id);
        User Update(User user);
    }
}
