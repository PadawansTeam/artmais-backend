using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.OAuth.Google;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.SignUp.Request;
using ArtmaisBackend.Core.Users.Dto;
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
            this._context = context;
        }

        private readonly ArtplusContext _context;

        public User Create(SignUpRequest signUpRequest, int userTypeId = 1)
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
                RegisterDate = DateTime.Now,
                UserPicture = signUpRequest.UserPicture,
                BackgroundPicture = signUpRequest.BackgroundPicture,
                Provider = "artmais",
                UserTypeId = userTypeId
            };

            this._context.User.Add(user);
            this._context.SaveChanges();

            _context.Entry(user).Reference(u => u.UserType).Load();

            return user;
        }

        public User CreateOAuthUser(OAuthSignUpRequest signUpRequest, string provider, int userTypeId = 1)
        {
            var user = new User
            {
                SubcategoryID = signUpRequest.SubcategoryID,
                Name = signUpRequest.Name,
                Email = signUpRequest.Email,
                Description = signUpRequest.Description,
                Username = signUpRequest.Username,
                BirthDate = signUpRequest.BirthDate,
                RegisterDate = DateTime.Now,
                UserPicture = signUpRequest.UserPicture,
                BackgroundPicture = signUpRequest.BackgroundPicture,
                Provider = provider,
                UserTypeId = userTypeId
            };

            this._context.User.Add(user);
            this._context.SaveChanges();

            _context.Entry(user).Reference(u => u.UserType).Load();

            return user;
        }

        public User GetUserByEmail(string email)
        {
            var query = from user in this._context.User
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
                            RegisterDate = user.RegisterDate,
                            UserTypeId = user.UserTypeId,
                            UserType = user.UserType
                        };

            return query.FirstOrDefault();
        }

        public IEnumerable<RecomendationDto> GetUsersByInterest(long userId)
        {
            var results = (from user in this._context.User
                           join interest in this._context.Interest on user.SubcategoryID equals interest.SubcategoryID
                           join subcategory in this._context.Subcategory on interest.SubcategoryID equals subcategory.SubcategoryID
                           join category in this._context.Category on subcategory.CategoryID equals category.CategoryID
                           where interest.UserID.Equals(userId)
                           && !user.UserID.Equals(userId)
                           && subcategory.OtherSubcategory.Equals(false)
                           select new RecomendationDto
                           {
                               UserId = user.UserID,
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
            return this._context.User.FirstOrDefault(user => user.Username == username);
        }

        public User GetUserById(long? id)
        {
            var user = this._context.User.FirstOrDefault(user => user.UserID == id);

            _context.Entry(user).Reference(u => u.UserType).Load();

            return user;
        }

        public User Update(User user)
        {
            this._context.User.Update(user);
            this._context.SaveChanges();

            return user;
        }

        public UserCategoryDto GetSubcategoryByUserId(long userId)
        {
            var query = from user in this._context.User
                        join subcategory in this._context.Subcategory on user.SubcategoryID equals subcategory.SubcategoryID
                        join category in this._context.Category on subcategory.CategoryID equals category.CategoryID
                        where user.UserID.Equals(userId)
                        select new UserCategoryDto
                        {
                            Category = category.UserCategory,
                            Subcategory = subcategory.UserSubcategory
                        };

            return query.FirstOrDefault();
        }

        public IEnumerable<RecomendationDto> GetUsers()
        {
            var results = (from user in this._context.User
                           join subcategory in this._context.Subcategory on user.SubcategoryID equals subcategory.SubcategoryID
                           join category in this._context.Category on subcategory.CategoryID equals category.CategoryID
                           select new RecomendationDto
                           {
                               UserId = user.UserID,
                               Username = user.Username,
                               UserPicture = user.UserPicture,
                               BackgroundPicture = user.BackgroundPicture,
                               Category = category.UserCategory,
                               Subcategory = subcategory.UserSubcategory
                           }).ToList();

            return results;
        }

        public IEnumerable<RecomendationDto> GetUsersByUsernameOrNameOrSubcategoryOrCategory(string searchValue)
        {
            var results = (from user in this._context.User
                           join subcategory in this._context.Subcategory on user.SubcategoryID equals subcategory.SubcategoryID
                           join category in this._context.Category on subcategory.CategoryID equals category.CategoryID
                           where user.Username.Contains(searchValue)
                           || user.Name.Contains(searchValue)
                           || subcategory.UserSubcategory.Contains(searchValue)
                           || category.UserCategory.Contains(searchValue)
                           || user.Description.Contains(searchValue)
                           select new RecomendationDto
                           {
                               UserId = user.UserID,
                               Username = user.Username,
                               UserPicture = user.UserPicture,
                               BackgroundPicture = user.BackgroundPicture,
                               Category = category.UserCategory,
                               Subcategory = subcategory.UserSubcategory
                           }).ToList();

            return results;
        }
    }
}
