using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Mvc;
using HelloCoreMvcApp.Models.ShoppingCart;
using HelloCoreMvcApp.Models.Utils;
using HelloCoreMvcApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

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
            string key = "shoppingcart";

            ShoppingCart shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString(key), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            return View(shoppingCart);
        }

        [HttpPost]
        public IActionResult AddCartItem(int itemId)
        {
            ShoppingCart shoppingCart = new ShoppingCart();
            string key = "shoppingcart";

            // Getting an item from db
            //
            Item item = db.Set<Item>().FirstOrDefault(p => p.Id == itemId);

            // Getting a ShoppingCart of current session
            //
            if (HttpContext.Session.Keys.Contains(key))
            {
                shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString(key), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }

            // Adding new item and storing into session
            //
            shoppingCart.AddCartItem(new ShoppingCartItem(item));
            HttpContext.Session.SetString(key, JsonConvert.SerializeObject(shoppingCart, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));

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
