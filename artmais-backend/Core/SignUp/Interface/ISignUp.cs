using System.Collections.Generic;

namespace artmais_backend.Core.SignUp.Interface
{
    public interface ISignUp
    {
        public IEnumerable<CategorySubcategoryDto> Index();
        public string Create(SignUpRequest signUpRequest);
    }
}
