using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Payment.Enum;
using ArtmaisBackend.Core.Payment.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Payment.Interface
{
    public interface IPaymentService
    {
        Task<bool> InsertPayment(long userId, PaymentStatusEnum PaymentStatusEnum);

        Task<bool> UpdatePayment(Entities.Payment paymentRequest);

        Task<Entities.Payment?> GetPaymentByUserId(long userId);

        Task<Entities.Payment?> GetPaymentByIdAndUserId(int paymentId, long userId);

        Task<bool> InsertPaymentHistory(int paymentId, PaymentStatusEnum paymentStatusEnum);

        Task<PaymentHistory?> GetPaymentHistoryByPaymentId(int paymentId);

        Task<bool> InsertPaymentProduct(int productId, int paymentId);

        Task<PaymentProduct?> GetPaymentProductByPaymentId(int paymentId);

        Task<Product?> GetSignature();

        Task<List<Product>> GetProductsByUserId(long userId);

        Task<PaymentStatus?> GetPaymentStatus(PaymentStatusEnum paymentStatusEnum);

        Task<PaymentType?> GetPaymentType(PaymentTypeEnum paymentTypeEnum);
    }
}
