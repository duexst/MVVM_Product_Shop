using System.Collections.Generic;

namespace MVVM_Product_Shop.DAL.Entities
{
    public interface IShoppingCart
    {
        void AddToCart(Product Product);
        int RemoveFromCart(Product Product);
        List<ShoppingCartItem> GetShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
