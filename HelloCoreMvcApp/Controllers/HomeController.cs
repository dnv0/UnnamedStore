using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HelloCoreMvcApp.Models;

namespace HelloCoreMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private MobileContext db;


        public HomeController(MobileContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            Random rnd = new Random();
            List<Phone> phones = db.Phones.ToList<Phone>();
            ViewData["ProductDay"] = phones[rnd.Next(0, phones.Count - 1)];
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

    }
}
