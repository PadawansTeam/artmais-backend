using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public PaymentRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public async Task<Payment?> Create(long userId, int paymentTypeEnum)
        {
            var date = DateTime.UtcNow;
            var payment = new Payment
            {
                UserID = userId,
                PaymentTypeID = paymentTypeEnum,
                CreateDate = date,
                LastUpdateDate = date
            };

            await _context.Payment.AddAsync(payment);
            _context.SaveChanges();

            return payment;
        }

        public async Task<Payment?> Update(Payment paymentRequest)
        {
            _context.Payment.Update(paymentRequest);
            await _context.SaveChangesAsync();

            return paymentRequest;
        }

        public async Task<Payment?> GetPaymentByUserId(long userId)
        {
            return await _context.Payment
                .Where(payment => payment.UserID == userId)
                .OrderByDescending(payment => payment.LastUpdateDate)
                .FirstOrDefaultAsync();
        }

        public async Task<Payment?> GetPaymentByIdAndUserId(int paymentId, long userId)
        {
            return await _context.Payment
                .Where(payment => payment.PaymentID == paymentId && payment.UserID == userId)
                .OrderByDescending(payment => payment.LastUpdateDate)
                .FirstOrDefaultAsync();
        }
    }
}