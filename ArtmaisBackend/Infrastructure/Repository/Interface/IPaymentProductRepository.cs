using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPaymentProductRepository
    {
        Task<PaymentProduct?> Create(int productId, int paymentId);

        Task<PaymentProduct?> GetPaymentProductByPaymentId(int paymentId);
    }
}
