using ArtmaisBackend.Core.SignUp.Dto;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.SignUp.Interface
{
    public interface ISignUp
    {
        IEnumerable<CategorySubcategoryDto> Index();
        string Create(SignUpRequest signUpRequest);
    }
}
