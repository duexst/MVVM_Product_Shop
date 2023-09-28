using Microsoft.EntityFrameworkCore;
using MVVM_Product_Shop.DAL.Entities;
using MVVM_Product_Shop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVVM_Product_Shop.Services
{
    public class ProductService
    {
        private readonly ProductShopDbContext _productShopDbContext;

        public ProductService(ProductShopDbContext _productShopDbContext)
        {
            this._productShopDbContext = _productShopDbContext;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync()
        {

            return await _productShopDbContext.Products.Select(
                p => new ProductModel
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Description,
                    IsFeaturedProduct = p.IsFeaturedProduct,
                    CategoryId = p.CategoryId,
                    Category = p.Category
                }
            ).ToListAsync();
        }

        public async Task<ProductModel> GetProductByIdAsync(int productId)
        {
            return await _productShopDbContext.Products.Select(
                    p => new ProductModel
                    {
                        ProductId = p.ProductId,
                        Name = p.Name,
                        Price = p.Price,
                        Description = p.Description,
                        IsFeaturedProduct = p.IsFeaturedProduct,
                        CategoryId = p.CategoryId,
                        Category = p.Category
                    })
                .FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task InsertProductAsync(ProductModel product)
        {
            var entity = new Product()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                IsFeaturedProduct = product.IsFeaturedProduct,
                CategoryId = product.CategoryId,
                Category = product.Category
            };

            _productShopDbContext.Products.Add(entity);
            await _productShopDbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int productId)
        {
            var entity = new Product()
            {
                ProductId = productId
            };
            _productShopDbContext.Products.Attach(entity);
            _productShopDbContext.Products.Remove(entity);
            await _productShopDbContext.SaveChangesAsync();
        }

    }
}

