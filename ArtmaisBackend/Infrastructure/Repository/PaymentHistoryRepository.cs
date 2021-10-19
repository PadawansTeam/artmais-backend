using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System;
using System.Collections.Generic;
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
    }
}
