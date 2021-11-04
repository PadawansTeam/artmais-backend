using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPaymentProductRepository
    {
        Task<PaymentProduct?> GetPaymentProductByPaymentId(int paymentId);
    }
}
