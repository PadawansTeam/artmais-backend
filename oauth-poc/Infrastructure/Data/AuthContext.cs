using Microsoft.EntityFrameworkCore;
using oauth_poc.Core.Entities;

namespace oauth_poc.Infrastructure.Data
{
    public class AuthContext : DbContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
    }
}

