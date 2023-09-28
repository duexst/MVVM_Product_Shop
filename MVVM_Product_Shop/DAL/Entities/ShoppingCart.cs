using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MVVM_Product_Shop.DAL.Entities 
{
    public class ShoppingCart : IShoppingCart
    {
        private readonly ProductShopDbContext _ProductShopDbContext;

        public string? ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

        private ShoppingCart(ProductShopDbContext ProductShopDbContext)
        {
            _ProductShopDbContext = ProductShopDbContext;
        }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

            ProductShopDbContext context = services.GetService<ProductShopDbContext>() ?? throw new Exception("Error initializing");

            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

            session?.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product Product)
        {
            var shoppingCartItem =
                    _ProductShopDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ProductId == Product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = Product,
                    Amount = 1
                };

                _ProductShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _ProductShopDbContext.SaveChanges();
        }

        public int RemoveFromCart(Product Product)
        {
            var shoppingCartItem =
                    _ProductShopDbContext.ShoppingCartItems.SingleOrDefault(
                        s => s.Product.ProductId == Product.ProductId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _ProductShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _ProductShopDbContext.SaveChanges();

            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??=
                       _ProductShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Product)
                           .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _ProductShopDbContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _ProductShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

            _ProductShopDbContext.SaveChanges();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _ProductShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
    }
}
