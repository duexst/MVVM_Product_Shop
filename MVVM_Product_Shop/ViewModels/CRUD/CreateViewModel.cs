﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using MVVM_Product_Shop.Models;
using MVVM_Product_Shop.Services;

namespace MVVM_Product_Shop.ViewModels.CRUD
{
    public class CreateViewModel : MasterPageViewModel
    {
        private readonly ProductService studentService;

        public ProductModel Product { get; set; } = new ProductModel {  };

        public CreateViewModel(ProductService studentService)
        {
            this.studentService = studentService;
        }


        public async Task AddProduct()
        {
            await studentService.InsertProductAsync(Product);
            Context.RedirectToRoute("Default");
        }
    }
}
