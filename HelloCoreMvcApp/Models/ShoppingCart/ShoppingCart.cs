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
            CartList.Add(item);
        }

        public void RemoveCartItem(ShoppingCartItem item)
        {
            CartList.Remove(item);
        }
    }
}
