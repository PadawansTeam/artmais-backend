using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Payments.Enums;
using ArtmaisBackend.Core.Payments.Request;
using MercadoPago.Resource.Payment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Payments.Interface
{
    public interface IPaymentService
    {
        Task<Payment> PaymentCreateRequest(PaymentRequest paymentRequest, long userId);
    }
}
