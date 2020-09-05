using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCoreMvcApp.Models.ShoppingCart
{
    public class ShoppingCartItem
    {
        public Item Product { get; set; }
        public int Quantity { get; set; } = 1;

        public ShoppingCartItem(Item item)
        {
            Product = item;
        }
    }
}
