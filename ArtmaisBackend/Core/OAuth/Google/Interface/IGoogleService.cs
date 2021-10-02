using Google.Apis.Auth;
using System.Threading.Tasks;

namespace ArtmaisBackend.Services.Interface
{
    public interface IGoogleService
    {
        Task<GoogleJsonWebSignature.Payload> ValidateToken(string token);
    }
}
