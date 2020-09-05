using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HelloCoreMvcApp.Models;
using HelloCoreMvcApp.Models.Products;
using Microsoft.EntityFrameworkCore;

namespace HelloCoreMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductContext db;


        public HomeController(ProductContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            // Returning a random item for jumbotrone
            //
            Random rnd = new Random();
            List<Phone> phones = db.Phones.Include(x => x.Company).ToList();
            ViewData["ProductDay"] = phones[rnd.Next(0, phones.Count - 1)];

            // Passing an items for main page
            //
            ViewData["Ships"] = db.Ships.Include(x => x.Company).ToList();
            ViewData["Papers"] = db.ToiletPapers.Include(x => x.Company).ToList();

            return View(db.Phones.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Catalog()
        {
            return View();
        }
    }
}
