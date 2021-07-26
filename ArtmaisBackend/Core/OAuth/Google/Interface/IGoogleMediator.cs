using System.Threading.Tasks;

namespace ArtmaisBackend.Core.OAuth.Google.Interface
{
    public interface IGoogleMediator
    {
        Task<bool> SignIn(string token);
    }
}
