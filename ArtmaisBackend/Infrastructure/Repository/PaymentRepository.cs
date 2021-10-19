using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        public PaymentRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;
    }
}
