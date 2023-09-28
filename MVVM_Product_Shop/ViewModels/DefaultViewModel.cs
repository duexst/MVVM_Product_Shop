using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using MVVM_Product_Shop.DAL.Entities;
using MVVM_Product_Shop.Models;
using MVVM_Product_Shop.Services;

namespace MVVM_Product_Shop.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

        private readonly ProductService productService;


        public List<ProductModel> Products { get; set; }
        public List<ProductModel> FilteredProducts { get; set; } = new List<ProductModel>();
        public List<ProductModel> ShoppingCart {  get; set; } = new List<ProductModel>();
        public string SearchQuery { get; set; }
        public Category SelectedCategory { get; set; }
        public List<Category> Categories { get; set; }

		public DefaultViewModel(ProductService productService)
        {
            this.productService = productService;
        }
        public override async Task PreRender()
        {
            if (!Context.IsPostBack)
            {
                Products = await productService.GetAllProductsAsync();
                FilteredProducts = Products;
                Categories = new List<Category>();
                foreach (ProductModel product in Products)
                {
                    if(!Categories.Contains(product.Category))
                        Categories.Add(product.Category);
                }
            }

            await base.PreRender();
        }

        public void FilterProductsByCategory()
        {
            if (String.IsNullOrEmpty(SelectedCategory?.CategoryName))
            {
                FilteredProducts = Products;
                return;
            }

            FilteredProducts = Products.FindAll(products => products.CategoryId == SelectedCategory.CategoryId);
        }

        
        public void FilterProductsByQuery()
        {

            if (string.IsNullOrEmpty(SearchQuery))
            {
                FilteredProducts = Products;
                return;
            }
            FilteredProducts = Products.FindAll(products => products.Name.ToLower().Contains(SearchQuery));
        }

        public void ClearFilter()
        {
            FilteredProducts = Products;
            SearchQuery = "";
        }

        public void AddProduct(ProductModel product)
        {
            ShoppingCart.Add(product);
        }

        public void RemoveProduct(ProductModel product)
        {
            ShoppingCart.Remove(product);
        }
    }
}
