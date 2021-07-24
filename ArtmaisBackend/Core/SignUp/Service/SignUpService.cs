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
    public class SignUpService : ISignUp
    {
        public SignUpService(IUserRepository userRepository, ICategorySubcategoryRepository categorySubcategoryRepository, IJwtToken jwtToken)
        {
            _userRepository = userRepository;
            _categorySubcategoryRepository = categorySubcategoryRepository;
            _jwtToken = jwtToken;
        }

        private readonly IUserRepository _userRepository;
        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IJwtToken _jwtToken;

        public IEnumerable<CategorySubcategoryDto> Index()
        {
            return _categorySubcategoryRepository.GetCategoryAndSubcategory();
        }

        public string Create(SignUpRequest signUpRequest)
        {
            var existentUserEmail = _userRepository.GetUserByEmail(signUpRequest.Email);

            if (existentUserEmail != null)
                throw new EmailAlreadyInUse("E-mail já utilizado.");

            var existentUsername = _userRepository.GetUserByUsername(signUpRequest.Username);

            if (existentUsername != null)
                throw new UsernameAlreadyInUse("Username já utilizado.");

            var existentSubcategory = _categorySubcategoryRepository
                .GetSubcategoryBySubcategory(signUpRequest.Subcategory);

            if (existentSubcategory == null)
                existentSubcategory = _categorySubcategoryRepository.Create(signUpRequest.Category, signUpRequest.Subcategory);

            signUpRequest.SubcategoryID = existentSubcategory.SubcategoryID;

            signUpRequest.Password = PasswordUtil.Encrypt(signUpRequest.Password);
            var user = _userRepository.Create(signUpRequest);

            return _jwtToken.GenerateToken(user);
        }
    }
}
