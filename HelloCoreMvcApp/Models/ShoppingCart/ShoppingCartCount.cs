using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Controllers;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Newtonsoft.Json;

namespace HelloCoreMvcApp.Models.ShoppingCart
{
    public class ShoppingCartCount : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            if (HttpContext.Session.Keys.Contains("shoppingcart"))
            {
                ShoppingCart shoppingCart = JsonConvert.DeserializeObject<ShoppingCart>(HttpContext.Session.GetString("shoppingcart"), new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                ViewData["CartCount"] = shoppingCart.CartList.Count;
            }

            return View("~/Views/ShoppingCart/_ShoppingCartCount.cshtml");
        }
    }
}
