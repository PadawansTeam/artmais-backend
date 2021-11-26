using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class PaymentProductRepository : IPaymentProductRepository
    {
        public PaymentProductRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public async Task<PaymentProduct?> Create(int productId, int paymentId)
        {
            var paymentProduct = new PaymentProduct
            {
                ProductID = productId,
                PaymentID = paymentId,
            };

            await _context.PaymentProduct.AddAsync(paymentProduct);
            _context.SaveChanges();

            return paymentProduct;
        }

        public async Task<PaymentProduct?> GetPaymentProductByPaymentId(int paymentId)
        {
            return await _context.PaymentProduct
                .Where(paymentProduct => paymentProduct.PaymentID == paymentId)
                .FirstOrDefaultAsync();
        }
    }
}
