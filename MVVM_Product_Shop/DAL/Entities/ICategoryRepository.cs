using System.Collections.Generic;

namespace MVVM_Product_Shop.DAL.Entities
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
