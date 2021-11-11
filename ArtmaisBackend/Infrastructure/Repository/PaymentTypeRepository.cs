using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Diagnostics.CodeAnalysis;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        public PaymentTypeRepository(ArtplusContext context)
        {
            _context = context;
        }
        private readonly ArtplusContext _context;
    }
}
