using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPaymentStatusRepository
    {
        Task<PaymentStatus?> GetPaymentStatusById(int? paymentStatusId);
    }
}
