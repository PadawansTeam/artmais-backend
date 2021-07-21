using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Profile;
using ArtmaisBackend.Core.SignUp;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArtmaisBackend.Infrastructure.Repository
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

        public User GetUserByEmail(string email)
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

        public IEnumerable<RecomendationDto> GetUsersByInterest(int userId)
        {
            var results = (from user in _context.User
                           join interest in _context.Interest on user.SubcategoryID equals interest.SubcategoryID
                           join subcategory in _context.Subcategory on interest.SubcategoryID equals subcategory.SubcategoryID
                           join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                           where interest.UserID.Equals(userId)
                           && !user.UserID.Equals(userId)
                           && subcategory.OtherSubcategory.Equals(0)
                           select new RecomendationDto
                           {
                               Username = user.Username,
                               UserPicture = user.UserPicture,
                               BackgroundPicture = user.BackgroundPicture,
                               Category = category.UserCategory,
                               Subcategory = subcategory.UserSubcategory
                           }).ToList();

            return results;
        }

        public User GetUserByUsername(string username)
        {
            var query = from user in _context.User
                        where user.Username.Equals(username)
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
