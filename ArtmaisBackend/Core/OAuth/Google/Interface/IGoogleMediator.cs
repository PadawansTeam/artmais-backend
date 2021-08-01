using System.Threading.Tasks;

namespace ArtmaisBackend.Core.OAuth.Google.Interface
{
    public interface IGoogleMediator
    {
        Task<string?> SignIn(string token);
        string SignUp(OAuthSignUpRequest request);
    }
}
