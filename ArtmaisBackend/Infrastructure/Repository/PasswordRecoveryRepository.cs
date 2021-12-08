using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class PasswordRecoveryRepository : IPasswordRecoveryRepository
    {
        private readonly ArtplusContext _context;

        public PasswordRecoveryRepository(ArtplusContext context)
        {
            _context = context;
        }

        public async Task<PasswordRecovery> CreateAsync(long userId, int code)
        {
            var passwordRecovery = new PasswordRecovery
            {
                UserID = userId,
                Code = code,
                ExpirationDate = DateTime.UtcNow.AddMinutes(15)
            };

            await _context.PasswordRecovery.AddAsync(passwordRecovery);
            await _context.SaveChangesAsync();

            return passwordRecovery;
        }

        public async Task<PasswordRecovery?> GetByUserIdAndCodeAsync(long userId, int code)
        {
            return await _context.PasswordRecovery
                .FirstOrDefaultAsync(passwordRecovery => passwordRecovery.UserID == userId && passwordRecovery.Code == code);
        }
    }
}
