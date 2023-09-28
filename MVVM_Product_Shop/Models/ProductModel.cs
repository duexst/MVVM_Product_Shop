using Microsoft.AspNetCore.Mvc.ModelBinding;
using MVVM_Product_Shop.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace MVVM_Product_Shop.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; } = "";
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public bool IsFeaturedProduct { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
