using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using DotVVM.Framework.Hosting;
using MVVM_Product_Shop.Models;
using MVVM_Product_Shop.Services;

namespace MVVM_Product_Shop.ViewModels.CRUD
{
    public class DetailViewModel : MasterPageViewModel
    {
        private readonly ProductService productService;
        
        public ProductModel Product { get; set; }

        [FromRoute("Id")]
        public int Id { get; private set; }

        public DetailViewModel(ProductService productService)
        {
            this.productService = productService;
        }

        public override async Task PreRender()
        {
            Product = await productService.GetProductByIdAsync(Id);
            await base.PreRender();
        }

        public async Task DeleteProduct()
        {
            await productService.DeleteProductAsync(Id);
            Context.RedirectToRoute("Default", replaceInHistory: true);
        }
    }
}
