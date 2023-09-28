using System.Collections.Generic;
using System.Linq;

namespace MVVM_Product_Shop.DAL.Entities
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductShopDbContext _productShopDbContext;

        public CategoryRepository(ProductShopDbContext produktShopDbContext)
        {
            _productShopDbContext = produktShopDbContext;
        }

        public IEnumerable<Category> AllCategories => _productShopDbContext.Categories.OrderBy(c => c.CategoryName);
    }
}
