using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.SignUp;
using System.Collections.Generic;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IUserRepository
    {
        public User Create(SignUpRequest signUpRequest);
        public User GetUserByEmail(string email);
        public IEnumerable<RecomendationDto> GetUsersByInterest(int userId);
        public User GetUserByUsername(string username);
    }
}
