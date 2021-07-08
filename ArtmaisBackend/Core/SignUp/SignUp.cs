using ArtmaisBackend.Core.SignIn.Interface;
using ArtmaisBackend.Core.SignUp.Interface;
using ArtmaisBackend.Exceptions;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using ArtmaisBackend.Util;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.SignUp
{
    public class SignUp : ISignUp
    {
        public SignUp(IUserRepository usuarioRepository, ICategorySubcategoryRepository categorySubcategoryRepository, IJwtToken jwtToken)
        {
            _usuarioRepository = usuarioRepository;
            _categorySubcategoryRepository = categorySubcategoryRepository;
            _jwtToken = jwtToken;
        }

        private readonly IUserRepository _usuarioRepository;
        private readonly ICategorySubcategoryRepository _categorySubcategoryRepository;
        private readonly IJwtToken _jwtToken;

        public IEnumerable<CategorySubcategoryDto> Index()
        {
            return _categorySubcategoryRepository.GetCategoryAndSubcategory();
        }

        public string Create(SignUpRequest signUpRequest)
        {
            var existentUser = _usuarioRepository.GetUsuarioByEmail(signUpRequest.Email);

            if (existentUser != null)
                throw new EmailAlreadyInUse("E-mail já utilizado");

            var existentSubcategory = _categorySubcategoryRepository
                .GetSubcategoryBySubcategory(signUpRequest.Subcategory);

            if (existentSubcategory == null)
                existentSubcategory = _categorySubcategoryRepository.Create(signUpRequest.Category, signUpRequest.Subcategory);

            signUpRequest.SubcategoryID = existentSubcategory.SubcategoryID;

            signUpRequest.Password = PasswordUtil.Encrypt(signUpRequest.Password);
            var user = _usuarioRepository.Create(signUpRequest);

            return _jwtToken.GenerateToken(user);
        }
    }
}
