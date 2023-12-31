﻿using System.Collections.Generic;

namespace MVVM_Product_Shop.DAL.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
    }
}
