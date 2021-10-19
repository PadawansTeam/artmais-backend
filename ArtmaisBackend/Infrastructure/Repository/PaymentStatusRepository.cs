using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class PaymentStatusRepository : IPaymentStatusRepository
    {
        public PaymentStatusRepository(ArtplusContext context)
        {
            _context = context;
        }
        private readonly ArtplusContext _context;
    }
}
