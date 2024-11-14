using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IProductRepository
    {

        public void AddProduct(Product product);
        public Task<Product?> GetProductByIdAsync(int id);
        public Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);

        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypesAsync();
        public bool ProductExists(int id);

        public Task<bool> SaveChangesAsync();
        public void DeleteProduct(Product product);
        public void UpdateProduct(Product product);


    }
}
