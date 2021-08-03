using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.SignUp.Dto;
using ArtmaisBackend.Core.SignUp.Interface;
using ArtmaisBackend.Core.SignUp.Request;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.SignUp.Service
{
    public class SignUpService : ISignUpService
    {
        public SignUpService(IUserRepository userRepository, ICategorySubcategoryRepository categorySubcategoryRepository, IJwtTokenService jwtToken)
        {
            this._userRepository = userRepository;
            this._categorySubcategoryRepository = categorySubcategoryRepository;
            this._jwtToken = jwtToken;
        }

        private readonly IUserRepository _userRepository;
        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IJwtTokenService _jwtToken;

        public IEnumerable<CategorySubcategoryDto> Index()
        {
            return this._categorySubcategoryRepository.GetCategoryAndSubcategory();
        }

        public string Create(SignUpRequest signUpRequest)
        {
            var existentUserEmail = this._userRepository.GetUserByEmail(signUpRequest.Email);

            if (existentUserEmail != null)
                throw new EmailAlreadyInUse("E-mail já utilizado.");

            var existentUsername = this._userRepository.GetUserByUsername(signUpRequest.Username);

            if (existentUsername != null)
                throw new UsernameAlreadyInUse("Username já utilizado.");

            var existentSubcategory = this._categorySubcategoryRepository
                .GetSubcategoryBySubcategory(signUpRequest.Subcategory);

            if (existentSubcategory == null)
                existentSubcategory = this._categorySubcategoryRepository.Create(signUpRequest.Category, signUpRequest.Subcategory);

            signUpRequest.SubcategoryID = existentSubcategory.SubcategoryID;

            signUpRequest.Password = PasswordUtil.Encrypt(signUpRequest.Password);

            if (signUpRequest.Category.Equals("Consumidor"))
                return CreateUser(signUpRequest, 2);

            return CreateUser(signUpRequest);
        }

        private string CreateUser(SignUpRequest signUpRequest, int userTypeId = 1)
        {
            var user = _userRepository.Create(signUpRequest, userTypeId);

            return this._jwtToken.GenerateToken(user);
        }
    }
}
