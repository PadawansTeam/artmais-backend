using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Diagnostics.CodeAnalysis;

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
    }
}
