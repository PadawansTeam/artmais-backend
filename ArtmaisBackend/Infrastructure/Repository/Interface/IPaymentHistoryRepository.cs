using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPaymentHistoryRepository
    {
        Task<PaymentHistory?> Create(int paymentId, int paymentStatusId);

        Task<PaymentHistory?> GetPaymentHistoryByPaymentId(int paymentId);
    }
}
