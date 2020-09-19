using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;
using HelloCoreMvcApp.Models.ShoppingCart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HelloCoreMvcApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.Keys.Contains("shoppingcart"))
            {
                ShoppingCart cart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString("shoppingcart"), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                return View("Order", cart.CartList);
            }

            return View("Order");
        }
    }
}
