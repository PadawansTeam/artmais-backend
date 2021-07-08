using artmais_backend.Core.Entities;
using artmais_backend.Core.SignUp;
using artmais_backend.Infrastructure.Data;
using artmais_backend.Infrastructure.Repository.Interface;
using System;
using System.Linq;

namespace artmais_backend.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public User Create(SignUpRequest signUpRequest)
        {
            var user = new User
            {
                SubcategoryID = signUpRequest.SubcategoryID,
                Name = signUpRequest.Name,
                Email = signUpRequest.Email,
                Password = signUpRequest.Password,
                Description = signUpRequest.Description,
                Username = signUpRequest.Username,
                BirthDate = signUpRequest.BirthDate,
                Role = signUpRequest.Role,
                RegisterDate = DateTime.Now
            };

            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetUsuarioByEmail(string email)
        {
            var query = from user in _context.User
                        where user.Email.Equals(email)
                        select new User
                        {
                            UserID = user.UserID,
                            SubcategoryID = user.SubcategoryID,
                            Name = user.Name,
                            Email = user.Email,
                            Password = user.Password,
                            Description = user.Description,
                            Username = user.Username,
                            BirthDate = user.BirthDate,
                            Role = user.Role,
                            RegisterDate = user.RegisterDate
                        };

            return query.FirstOrDefault();
        }
    }
}
