using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using MVVM_Product_Shop.DAL.Entities;
using MVVM_Product_Shop.Models;
using MVVM_Product_Shop.Services;

namespace MVVM_Product_Shop.ViewModels.CRUD
{
    public class CreateViewModel : MasterPageViewModel
    {
        private readonly ProductService productService;
        public List<Category> Categories { get; set; }
        public Category SelectedCategory { get; set; }
        public ProductModel Product { get; set; }

        public CreateViewModel(ProductService productService)
        {
            this.productService = productService;
            Product = new ProductModel();
        }

        public override async Task PreRender()
        {
            Categories = await productService.GetCategoriesAsync();

            await base.PreRender();
        }

        public async Task AddProduct()
        {
            Product.Category = SelectedCategory;
            await productService.InsertProductAsync(Product);
            Context.RedirectToRoute("Default");
        }
    }
}
