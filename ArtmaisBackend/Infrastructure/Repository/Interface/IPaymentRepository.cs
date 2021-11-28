using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<Payments?> Create(long userId, int paymentTypeEnum, long? externalPaymentId);

        Task<Payments?> Update(Payments paymentRequest);

        Task<Payments?> GetPaymentByUserId(long userId);

        Task<Payments?> GetPaymentByIdAndUserId(int paymentId, long userId);
    }
}
