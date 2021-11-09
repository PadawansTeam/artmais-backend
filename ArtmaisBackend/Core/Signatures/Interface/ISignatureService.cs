using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Signatures.Interface
{
    public interface ISignatureService
    {
        Task CreateSignature(long userId);

        Task UpdateSignature(long userId);

        Task<bool> GetSignatureByUserId(long userId);
    }
}
