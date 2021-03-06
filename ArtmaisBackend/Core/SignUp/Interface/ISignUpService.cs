using ArtmaisBackend.Core.SignUp.Dto;
using ArtmaisBackend.Core.SignUp.Request;
using System.Collections.Generic;

namespace ArtmaisBackend.Core.SignUp.Interface
{
    public interface ISignUpService
    {
        IEnumerable<CategorySubcategoryDto> Index();
        string Create(SignUpRequest signUpRequest);
    }
}
