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
                Name = signUpRequest.Name,
                Email = signUpRequest.Email,
                Password = signUpRequest.Password,
                RegisterDate = DateTime.Now
            };

            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetUsuarioByEmail(string email)
        {
            var query = from usuario in _context.User
                        where usuario.Email.Equals(email)
                        select new User
                        {
                            ID = usuario.ID,
                            Name = usuario.Name,
                            Email = usuario.Email,
                            Password = usuario.Password,
                            RegisterDate = usuario.RegisterDate
                        };

            return query.FirstOrDefault();
        }
    }
}
