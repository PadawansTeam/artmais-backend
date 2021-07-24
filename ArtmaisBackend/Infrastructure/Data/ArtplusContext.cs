using ArtmaisBackend.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtmaisBackend.Infrastructure.Data
{
    public class ArtplusContext : DbContext
    {
        public ArtplusContext(DbContextOptions<ArtplusContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }
        public DbSet<Interest> Interest { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}

