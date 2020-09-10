using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCoreMvcApp.Models.ShoppingCart
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> CartList { get; set; }

        public ShoppingCart()
        {
            CartList = new List<ShoppingCartItem>();
        }

        public void AddCartItem(ShoppingCartItem item)
        {
            ShoppingCartItem product = CartList.FirstOrDefault(p => p.Product.Id == item.Product.Id);

            if (product != null) product.Quantity++;
            else CartList.Add(item);
        }

        public void RemoveCartItemFromList(int index)
        {
            CartList.RemoveAt(index);
        }
    }
}
