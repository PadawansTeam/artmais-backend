using ArtmaisBackend.Core.Payment.Enums;
using System.Threading.Tasks;

namespace ArtmaisBackend.Core.Payment.Interface
{
    public interface IPaymentService
    {
        Task<bool> InsertPayment(long userId, PaymentStatusEnum PaymentStatusEnum);
    }
}
