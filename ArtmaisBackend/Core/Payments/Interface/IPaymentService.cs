using ArtmaisBackend.Core.Payments.Request;
using MercadoPago.Resource.Payment;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Payments.Interface
{
    public interface IPaymentService
    {
        Task<Payment> PaymentCreateRequest(PaymentRequest paymentRequest, long userId);
        Task UpdatePaymentAsync(long id);
    }
}
