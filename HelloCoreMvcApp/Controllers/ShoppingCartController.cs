using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using HelloCoreMvcApp.Models.ShoppingCart;
using HelloCoreMvcApp.Models.Utils;
using HelloCoreMvcApp.Models;

namespace HelloCoreMvcApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        ProductContext db;

        public ShoppingCartController(ProductContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCartItem(int itemId)
        {
            ShoppingCart shoppingCart = new ShoppingCart();

            // Getting an item from db
            //


            // Gettin a ShoppingCart of current session
            //
            if (HttpContext.Session.Keys.Contains("shoppingcart"))
            {
                shoppingCart = HttpContext.Session.Get<ShoppingCart>("shoppingcart");
            }

            // Adding new item and storing into session
            //
     //     shoppingCart.AddCartItem(new ShoppingCartItem(item));    //   Needs refactoring
            HttpContext.Session.Set<ShoppingCart>("shoppingcart", shoppingCart);

            return PartialView();
        }

        private Type GetEntityType(string name)
        {
            var entityTypes = db.Model.GetEntityTypes().Select(t => t.ClrType).ToList();
            foreach (var item in entityTypes)
            {
                if (item.Name.ToString() == name)
                    return item;
            }

            return null;
        }
    }
}
