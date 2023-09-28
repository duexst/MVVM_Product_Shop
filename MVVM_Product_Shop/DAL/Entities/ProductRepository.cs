using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MVVM_Product_Shop.DAL.Entities
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductShopDbContext _productShopDbContext;

        public ProductRepository(ProductShopDbContext productShopDbContext)
        {
            _productShopDbContext = productShopDbContext;
        }

        public IEnumerable<Product> AllProducts
        {
            get
            {
                return _productShopDbContext.Products.Include(p => p.Category);
            }
        }

        public IEnumerable<Product> FeaturedProducts
        {
            get
            {
                return _productShopDbContext.Products.Include(p => p.Category).Where(p => p.IsFeaturedProduct);
            }
        }

        public Product? GetProductById(int productId)
        {
            return _productShopDbContext.Products.FirstOrDefault(p => p.ProductId == productId);
        }

        public IEnumerable<Product> GetProductsForCategory(string? categoryName)
        {
            return _productShopDbContext.Products.Where(p => p.Category.CategoryName == categoryName)
                    .OrderBy(p => p.ProductId);
        }

        public IEnumerable<Product> SearchProducts(string searchQuery)
        {
            return _productShopDbContext.Products.Where(p => p.Name.Contains(searchQuery)).OrderBy(p => p.ProductId);
        }

        public void AddProduct(Product product)
        {
            _productShopDbContext.Add(product);
            _productShopDbContext.SaveChanges();
        }
    }
}
