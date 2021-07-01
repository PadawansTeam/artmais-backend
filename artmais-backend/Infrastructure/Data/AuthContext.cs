using Microsoft.EntityFrameworkCore;
using artmais_backend.Core.Entities;

namespace artmais_backend.Infrastructure.Data
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
    }
}

