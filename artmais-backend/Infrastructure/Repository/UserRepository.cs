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
        public UserRepository(AuthContext context)
        {
            _context = context;
        }

        private readonly AuthContext _context;

        public User Create(SignUpRequest signUpRequest)
        {
            var usuario = new User
            {
                Name = signUpRequest.Name,
                Surname = signUpRequest.Surname,
                SocialName = signUpRequest.SocialName,
                Email = signUpRequest.Email,
                Password = signUpRequest.Password,
                RegisterDate = DateTime.Now
            };

            _context.User.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        public User GetUsuarioByEmail(string email)
        {
            var query = from usuario in _context.User
                        where usuario.Email.Equals(email)
                        select new User
                        {
                            ID = usuario.ID,
                            Name = usuario.Name,
                            Surname = usuario.Surname,
                            SocialName = usuario.SocialName,
                            Email = usuario.Email,
                            Password = usuario.Password,
                            RegisterDate = usuario.RegisterDate
                        };

            return query.FirstOrDefault();
        }
    }
}
