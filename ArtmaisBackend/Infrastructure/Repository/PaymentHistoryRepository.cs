using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class PaymentHistoryRepository : IPaymentHistoryRepository
    {
        public PaymentHistoryRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public async Task<PaymentHistory?> Create(int paymentId, int paymentStatusId)
        {
            var paymentHistory = new PaymentHistory
            {
                PaymentID = paymentId,
                CreateDate = DateTime.UtcNow,
                PaymentStatusID = paymentStatusId,
            };

            await _context.PaymentHistory.AddAsync(paymentHistory);
            _context.SaveChanges();

            return paymentHistory;
        }

        public async Task<PaymentHistory?> GetPaymentHistoryByPaymentId(int paymentId)
        {
            return await _context.PaymentHistory
                .Where(paymentHistory => paymentHistory.PaymentID == paymentId)
                .OrderByDescending(paymentHistory => paymentHistory.CreateDate)
                .FirstOrDefaultAsync();
        }
    }
}
