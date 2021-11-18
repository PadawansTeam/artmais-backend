using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        public PaymentTypeRepository(ArtplusContext context)
        {
            _context = context;
        }
        private readonly ArtplusContext _context;

        public async Task<PaymentType?> GetPaymentTypeById(int? paymentTypeId)
        {
            return await _context.PaymentType.FirstOrDefaultAsync(paymentType => paymentType.PaymentTypeID == paymentTypeId);
        }
    }
}
