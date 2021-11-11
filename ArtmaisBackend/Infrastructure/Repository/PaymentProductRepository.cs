﻿using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using System.Diagnostics.CodeAnalysis;

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
    }
}
