using ArtmaisBackend.Core.SignUp.Dto;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.SignUp.Interface
{
    public interface ISignUp
    {
        public IEnumerable<CategorySubcategoryDto> Index();
        public string Create(SignUpRequest signUpRequest);
    }
}
