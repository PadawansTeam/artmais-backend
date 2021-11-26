using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    [ExcludeFromCodeCoverage]
    public class SignatureRepository : ISignatureRepository
    {
        public SignatureRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;

        public async Task Create(long userId)
        {
            var startDate = DateTime.UtcNow;
            var endDate = startDate.AddYears(1);
            var signatureContent = new Signature
            {
                UserID = userId,
                StartDate = startDate,
                EndDate = endDate
            };

            await _context.Signature.AddAsync(signatureContent);
            _context.SaveChanges();
        }

        public async Task<Signature> Update(Signature signature)
        {
            this._context.Signature.Update(signature);
            await this._context.SaveChangesAsync();

            return signature;
        }

        public async Task<Signature> GetSignatureByUserId(long userId)
        {
            return await _context.Signature.FirstOrDefaultAsync(signature => signature.UserID == userId);
        }
    }
}
