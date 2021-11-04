using ArtmaisBackend.Core.Entities;
using ArtmaisBackend.Core.Payment.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtmaisBackend.Infrastructure.Repository.Interface
{
    public interface IProductRepository
    {
        Task<Product?> Create(ProductRequest productRequest);

        Task<Product?> Update(Product productRequest);

        Task<List<Product>> GetProductsByUserId(long userId);

        Task<Product?> GetSignature();
    }
}
