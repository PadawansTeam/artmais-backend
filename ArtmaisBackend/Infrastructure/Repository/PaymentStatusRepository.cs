using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        public PaymentStatusRepository(ArtplusContext context)
        {
            _context = context;
        }
        private readonly ArtplusContext _context;

        public async Task<PaymentsStatus?> GetPaymentStatusById(int? paymentStatusId)
        {
            return await _context.PaymentStatus.FirstOrDefaultAsync(paymentStatus => paymentStatus.PaymentStatusID == paymentStatusId);
        }
    }
}
