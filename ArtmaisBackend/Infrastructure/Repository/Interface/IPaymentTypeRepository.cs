using ArtmaisBackend.Core.Entities;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IPaymentTypeRepository
    {
        Task<PaymentType?> GetPaymentTypeById(int? paymentTypeId);
    }
}
