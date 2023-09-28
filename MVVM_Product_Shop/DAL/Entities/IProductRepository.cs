using System.Collections.Generic;

namespace MVVM_Product_Shop.DAL.Entities
{
    public interface IProductRepository
    {
        IEnumerable<Product> AllProducts { get; }
        IEnumerable<Product> FeaturedProducts { get; }

        Product? GetProductById(int productId);
        IEnumerable<Product> GetProductsForCategory(string? categoryName);
        IEnumerable<Product> SearchProducts(string searchQuery);

        void AddProduct(Product product);
    }
}
