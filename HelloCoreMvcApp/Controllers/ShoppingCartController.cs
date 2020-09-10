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
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HelloCoreMvcApp.Controllers
{
    public class ShoppingCartController : Controller
    {
        ProductContext db;

        private string key = "shoppingcart"; // Key of shopping cart in session

        public ShoppingCartController(ProductContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ShoppingCart shoppingCart = GetCartFromSession();

            if (shoppingCart != null) return View(shoppingCart);
            else return View(new ShoppingCart());
        }

        [HttpPost]
        public IActionResult AddCartItem(int itemId)
        {
            ShoppingCart shoppingCart;

            // Getting an item from db
            //
            Item item = db.Set<Item>().Include(c => c.Company).FirstOrDefault(p => p.Id == itemId);

            // Getting a ShoppingCart of current session
            //
            shoppingCart = GetCartFromSession();
            if (shoppingCart == null) shoppingCart = new ShoppingCart();

            // Adding new item and storing into session
            //
            shoppingCart.AddCartItem(new ShoppingCartItem(item));
            HttpContext.Session.SetString(key, JsonConvert.SerializeObject(shoppingCart, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            }));

            // Redirect to the same page
            //
            return Redirect(Request.Headers["Referer"].ToString());
        }

        /// <summary>
        /// Method returns ShoppingCart object from session
        /// </summary>
        private ShoppingCart GetCartFromSession()
        {
            if (HttpContext.Session.Keys.Contains(key))
            {
                return JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString(key), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });
            }
            else return null;
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
