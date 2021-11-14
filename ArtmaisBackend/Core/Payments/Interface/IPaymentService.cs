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
        
        Task<bool> InsertPayment(long userId, PaymentStatusEnum PaymentStatusEnum);

        Task<bool> UpdatePayment(Entities.Payments paymentRequest);

        Task<Entities.Payments?> GetPaymentByUserId(long userId);

        Task<Entities.Payments?> GetPaymentByIdAndUserId(int paymentId, long userId);

        Task<bool> InsertPaymentHistory(int paymentId, PaymentStatusEnum paymentStatusEnum);

        Task<PaymentHistory?> GetPaymentHistoryByPaymentId(int paymentId);

        Task<bool> InsertPaymentProduct(int productId, int paymentId);

        Task<PaymentProduct?> GetPaymentProductByPaymentId(int paymentId);

        Task<Product?> GetSignature();

        Task<List<Product>> GetProductsByUserId(long userId);

        Task<PaymentsStatus?> GetPaymentStatus(PaymentStatusEnum paymentStatusEnum);

        Task<PaymentType?> GetPaymentType(PaymentTypeEnum paymentTypeEnum);
    }
}
