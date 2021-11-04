using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        public PaymentStatusRepository(ArtplusContext context)
        {
            _context = context;
        }
        private readonly ArtplusContext _context;

        public async Task<PaymentStatus?> GetPaymentStatusById(int? paymentStatusId)
        {
            return await _context.PaymentStatus.FirstOrDefaultAsync(paymentStatus => paymentStatus.PaymentStatusID == paymentStatusId);
        }
    }
}
