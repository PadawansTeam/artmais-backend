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
        public DbSet<ExternalAuthorization> ExternalAuthorization { get; set; }
        public DbSet<ProfileAccess> ProfileAccess { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<Publication> Publication { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Recommendation> Recommendation { get; set; }
        public DbSet<Payments> Payment { get; set; }
        public DbSet<PaymentHistory> PaymentHistory { get; set; }
        public DbSet<PaymentsStatus> PaymentStatus { get; set; }
        public DbSet<PaymentType> PaymentType { get; set; }
        public DbSet<PaymentProduct> PaymentProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Signature> Signature { get; set; }
        public DbSet<PasswordRecovery> PasswordRecovery { get; set; }
    }
}

