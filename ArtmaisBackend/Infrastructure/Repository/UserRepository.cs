using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.OAuth.Google;
using ArtmaisBackend.Core.Profile.Dto;
using ArtmaisBackend.Core.SignIn;
using ArtmaisBackend.Core.SignUp.Request;
using ArtmaisBackend.Core.Users.Dto;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
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

            _context.User.Add(user);
            _context.SaveChanges();

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

            _context.User.Add(user);
            _context.SaveChanges();

            _context.Entry(user).Reference(u => u.UserType).Load();

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
                            RegisterDate = user.RegisterDate,
                            UserTypeId = user.UserTypeId,
                            UserType = user.UserType
                        };

            return query.FirstOrDefault();
        }

        public IEnumerable<RecomendationDto> GetUsersByInterest(long userId)
        {
            var userSignature =
                (from signature in _context.Signature
                 join user in _context.User on signature.UserID equals user.UserID
                 join interest in _context.Interest on user.SubcategoryID equals interest.SubcategoryID
                 join subcategory in _context.Subcategory on interest.SubcategoryID equals subcategory.SubcategoryID
                 join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                 where
                 interest.UserID.Equals(userId)
                 && !user.UserID.Equals(userId)
                 && subcategory.OtherSubcategory.Equals(false)
                 select new RecomendationDto
                 {
                     UserId = user.UserID,
                     Name = user.Name,
                     Username = user.Username,
                     UserPicture = user.UserPicture,
                     BackgroundPicture = user.BackgroundPicture,
                     Category = category.UserCategory,
                     Subcategory = subcategory.UserSubcategory,
                     IsPremium = true
                 }).ToList(); 

            var recomendationQuery =
                (from user in _context.User
                 join recomendation in _context.Recommendation on user.SubcategoryID equals recomendation.SubcategoryID
                 join subcategory in _context.Subcategory on recomendation.SubcategoryID equals subcategory.SubcategoryID
                 join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                 where
                 recomendation.Interest.UserID.Equals(userId)
                 && !user.UserID.Equals(userId)
                 && subcategory.OtherSubcategory.Equals(false)
                 select new RecomendationDto
                 {
                     UserId = user.UserID,
                     Name = user.Name,
                     Username = user.Username,
                     UserPicture = user.UserPicture,
                     BackgroundPicture = user.BackgroundPicture,
                     Category = category.UserCategory,
                     Subcategory = subcategory.UserSubcategory,
                     IsPremium = false
                 }).Distinct();

            var interestQuery =
                (from user in _context.User
                 join interest in _context.Interest on user.SubcategoryID equals interest.SubcategoryID
                 join subcategory in _context.Subcategory on interest.SubcategoryID equals subcategory.SubcategoryID
                 join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                 where
                 interest.UserID.Equals(userId)
                 && !user.UserID.Equals(userId)
                 && subcategory.OtherSubcategory.Equals(false)
                 select new RecomendationDto
                 {
                     UserId = user.UserID,
                     Name = user.Name,
                     Username = user.Username,
                     UserPicture = user.UserPicture,
                     BackgroundPicture = user.BackgroundPicture,
                     Category = category.UserCategory,
                     Subcategory = subcategory.UserSubcategory,
                     IsPremium = false
                 });

            return userSignature.Union(recomendationQuery).Union(interestQuery).OrderBy(x => x.IsPremium == false).GroupBy(x => x.UserId).Select(y => y.First());
        }

        public User GetUserByUsername(string username)
        {
            return _context.User.FirstOrDefault(user => user.Username == username);
        }

        public User GetUserById(long? id)
        {
            var user = _context.User.FirstOrDefault(user => user.UserID == id);

            _context.Entry(user).Reference(u => u.UserType).Load();

            return user;
        }

        public User Update(User user)
        {
            _context.User.Update(user);
            _context.SaveChanges();

            return user;
        }

        public UserCategoryDto GetSubcategoryByUserId(long userId)
        {
            var query =
                from user in _context.User
                join subcategory in _context.Subcategory on user.SubcategoryID equals subcategory.SubcategoryID
                join category in _context.Category on subcategory.CategoryID equals category.CategoryID
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
            var recommandation =
                (from user in _context.User
                 join subcategory in _context.Subcategory on user.SubcategoryID equals subcategory.SubcategoryID
                 join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                 select new RecomendationDto
                 {
                     UserId = user.UserID,
                     Name = user.Name,
                     Username = user.Username,
                     UserPicture = user.UserPicture,
                     BackgroundPicture = user.BackgroundPicture,
                     Category = category.UserCategory,
                     Subcategory = subcategory.UserSubcategory,
                     IsPremium = false
                 }).ToList();

            var userSignature =
                (from signature in _context.Signature
                 join user in _context.User on signature.UserID equals user.UserID
                 join subcategory in _context.Subcategory on user.SubcategoryID equals subcategory.SubcategoryID
                 join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                 select new RecomendationDto
                 {
                     UserId = user.UserID,
                     Name = user.Name,
                     Username = user.Username,
                     UserPicture = user.UserPicture,
                     BackgroundPicture = user.BackgroundPicture,
                     Category = category.UserCategory,
                     Subcategory = subcategory.UserSubcategory,
                     IsPremium = true
                 }).ToList();

            return recommandation.Union(userSignature).OrderBy(x => x.IsPremium == false).GroupBy(x => x.UserId).Select(y => y.First());
        }

        public IEnumerable<RecomendationDto> GetUsersByUsernameOrNameOrSubcategoryOrCategory(string searchValue)
        {
            var recommandationSearch =
                (from user in _context.User
                 join subcategory in _context.Subcategory on user.SubcategoryID equals subcategory.SubcategoryID
                 join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                 where user.Username.ToUpper().Contains(searchValue.ToUpper())
                 || user.Name.ToUpper().Contains(searchValue.ToUpper())
                 || subcategory.UserSubcategory.ToUpper().Contains(searchValue.ToUpper())
                 || category.UserCategory.ToUpper().Contains(searchValue.ToUpper())
                 || user.Description.ToUpper().Contains(searchValue.ToUpper())
                 select new RecomendationDto
                 {
                     UserId = user.UserID,
                     Name = user.Name,
                     Username = user.Username,
                     UserPicture = user.UserPicture,
                     BackgroundPicture = user.BackgroundPicture,
                     Category = category.UserCategory,
                     Subcategory = subcategory.UserSubcategory,
                     IsPremium = false
                 }).ToList();

            var userSignature =
                (from signature in _context.Signature
                 join user in _context.User on signature.UserID equals user.UserID
                 join subcategory in _context.Subcategory on user.SubcategoryID equals subcategory.SubcategoryID
                 join category in _context.Category on subcategory.CategoryID equals category.CategoryID
                 where user.Username.ToUpper().Contains(searchValue.ToUpper())
                 || user.Name.ToUpper().Contains(searchValue.ToUpper())
                 || subcategory.UserSubcategory.ToUpper().Contains(searchValue.ToUpper())
                 || category.UserCategory.ToUpper().Contains(searchValue.ToUpper())
                 || user.Description.ToUpper().Contains(searchValue.ToUpper())
                 select new RecomendationDto
                 {
                     UserId = user.UserID,
                     Name = user.Name,
                     Username = user.Username,
                     UserPicture = user.UserPicture,
                     BackgroundPicture = user.BackgroundPicture,
                     Category = category.UserCategory,
                     Subcategory = subcategory.UserSubcategory,
                     IsPremium = true
                 }).ToList();

            return recommandationSearch.Union(userSignature).OrderBy(x => x.IsPremium == false).GroupBy(x => x.UserId).Select(y => y.First());
        }

        public bool ValidateUserData(UserJwtData userJwtData)
        {
            return _context.User.Any(u => u.UserID.Equals(userJwtData.UserID) && u.Username.Equals(userJwtData.UserName));
        }
    }
}
