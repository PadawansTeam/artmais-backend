using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class PaymentRepository : IPaymentRepository
    {
        public PaymentRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public async Task<Payments?> Create(long userId, int paymentTypeEnum)
        {
            var date = DateTime.UtcNow;
            var payment = new Payments
            {
                UserID = userId,
                PaymentTypeID = paymentTypeEnum,
                CreateDate = date,
                LastUpdateDate = date,
                ExternalPaymentID = 1
            };

            await _context.Payment.AddAsync(payment);
            _context.SaveChanges();

            return payment;
        }

        public async Task<Payments?> Update(Payments paymentRequest)
        {
            _context.Payment.Update(paymentRequest);
            await _context.SaveChangesAsync();

            return paymentRequest;
        }

        public async Task<Payments?> GetPaymentByUserId(long userId)
        {
            return await _context.Payment
                .Where(payment => payment.UserID == userId)
                .OrderByDescending(payment => payment.LastUpdateDate)
                .FirstOrDefaultAsync();
        }

        public async Task<Payments?> GetPaymentByIdAndUserId(int paymentId, long userId)
        {
            return await _context.Payment
                .Where(payment => payment.PaymentID == paymentId && payment.UserID == userId)
                .OrderByDescending(payment => payment.LastUpdateDate)
                .FirstOrDefaultAsync();
        }
    }
}