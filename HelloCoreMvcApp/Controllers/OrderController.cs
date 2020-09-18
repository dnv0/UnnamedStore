using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloCoreMvcApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductContext db;
        public OrderController(ProductContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View("Order");
        }

        [Route("[controller]/[action]")]
        public IActionResult NewOrder(List<Item> itemList)
        {


            return View(itemList);
        }

    }
}
