using MercadoPago.Client;
using MercadoPago.Client.Payment;
using System.Threading;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Payments.Interface
{
    public class MercadoPagoPaymentClient : PaymentClient, IMercadoPagoPaymentClient { }

    public interface IMercadoPagoPaymentClient
    {
        public Task<MercadoPago.Resource.Payment.Payment> CreateAsync(PaymentCreateRequest request, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
        public Task<MercadoPago.Resource.Payment.Payment> GetAsync(long id, RequestOptions requestOptions = null, CancellationToken cancellationToken = default);
    }
}
