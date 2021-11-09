using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface ISignatureRepository
    {
        Task Create(long userId);

        Task<Signature> GetSignatureByUserId(long userId);
    }
}
