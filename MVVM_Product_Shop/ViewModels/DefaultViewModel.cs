using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using MVVM_Product_Shop.Models;
using MVVM_Product_Shop.Services;

namespace MVVM_Product_Shop.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

        private readonly ProductService productService;

        [Bind(Direction.ServerToClient)]
        public List<ProductModel> Products { get; set; }

		public DefaultViewModel(ProductService productService)
        {
            this.productService = productService;
        }
        public override async Task PreRender()
        {
            Products =  await productService.GetAllProductsAsync();
            await base.PreRender();
        }

    }
}
