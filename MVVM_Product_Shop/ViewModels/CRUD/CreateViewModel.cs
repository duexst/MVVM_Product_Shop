using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Stopwatch _stopwatch;
        public List<Category> Categories { get; set; }
        public Category SelectedCategory { get; set; }
        public ProductModel Product { get; set; }

        public CreateViewModel(ProductService productService)
        {
            this.productService = productService;
            Product = new ProductModel();
            this._stopwatch = new Stopwatch();
        }

        public override async Task PreRender()
        {
            Categories = await productService.GetCategoriesAsync();

            await base.PreRender();
        }

        public async Task AddProduct()
        {
            _stopwatch.Restart();
            Product.Category = SelectedCategory;
            await productService.InsertProductAsync(Product);
            _stopwatch.Stop();
            Context.RedirectToRoute("Default");          
        }
    }
}
