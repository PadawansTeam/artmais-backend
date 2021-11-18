using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Payments.Request;
using ArtmaisBackend.Infrastructure.Data;
using ArtmaisBackend.Infrastructure.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(ArtplusContext context)
        {
            _context = context;
        }

        private readonly ArtplusContext _context;


        public async Task<Product?> Create(ProductRequest productRequest)
        {
            var product = new Product
            {
                UserID = productRequest.UserID,
                Name = productRequest.Name,
                Value = productRequest.Value
            };

            await _context.Product.AddAsync(product);
            _context.SaveChanges();

            return product;
        }

        public async Task<Product?> Update(Product productRequest)
        {
            _context.Product.Update(productRequest);
            await _context.SaveChangesAsync();

            return productRequest;
        }

        public async Task<List<Product>> GetProductsByUserId(long userId)
        {
            return await _context.Product.Where(product => product.UserID == userId).ToListAsync();
        }

        public async Task<Product?> GetSignature()
        {
            return await _context.Product
                .Where(product => product.ProductID == 1)
                .FirstOrDefaultAsync();
        }
    }
}
