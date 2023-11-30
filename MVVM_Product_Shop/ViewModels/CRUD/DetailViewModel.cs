using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using MVVM_Product_Shop.Models;
using MVVM_Product_Shop.Services;
using System.Diagnostics;

namespace MVVM_Product_Shop.ViewModels.CRUD
{
    public class DetailViewModel : MasterPageViewModel
    {
        private readonly ProductService productService;
        private Stopwatch _stopwatch;
        
        public ProductModel Product { get; set; }

        [FromRoute("Id")]
        public int Id { get; private set; }

        public DetailViewModel(ProductService productService)
        {
            this.productService = productService;
            this._stopwatch = new Stopwatch();
        }

        public override async Task PreRender()
        {
            _stopwatch.Restart();
            Product = await productService.GetProductByIdAsync(Id);
            await base.PreRender();
            _stopwatch.Stop();
        }

        public async Task DeleteProduct()
        {
            await productService.DeleteProductAsync(Id);
            Context.RedirectToRoute("Default", replaceInHistory: true);
        }
    }
}
