using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MVVM_Product_Shop.DAL.Entities
{
    public class Product
    {
        [BindNever]
        public int ProductId { get; set; }
        [BindRequired]
        public string Name { get; set; } = "";
        public string? Description { get; set; }
        [BindRequired]
        public decimal Price { get; set; }
        public bool IsFeaturedProduct { get; set; }
        [BindRequired]
        public int CategoryId { get; set; }
        [BindNever]
        public Category Category { get; set; } = default!;
    }
}
