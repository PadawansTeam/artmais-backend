using Microsoft.EntityFrameworkCore;
using artmais_backend.Core.Entities;

namespace artmais_backend.Infrastructure.Data
{
    public class ArtplusContext : DbContext
    {
        public ArtplusContext(DbContextOptions<ArtplusContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }
    }
}

