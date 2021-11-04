using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPaymentRepository
    {
        Task<Payment?> Create(long userId, int paymentTypeEnum);

        Task<Payment?> Update(Payment paymentRequest);

        Task<Payment?> GetPaymentByUserId(long userId);

        Task<Payment?> GetPaymentByIdAndUserId(int paymentId, long userId);
    }
}
