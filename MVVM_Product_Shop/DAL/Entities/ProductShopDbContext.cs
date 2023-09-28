using Microsoft.EntityFrameworkCore;

namespace MVVM_Product_Shop.DAL.Entities
{
    public class ProductShopDbContext : DbContext
    {
        public ProductShopDbContext(DbContextOptions<ProductShopDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
