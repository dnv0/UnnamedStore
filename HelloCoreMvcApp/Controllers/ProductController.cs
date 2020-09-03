using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;
using HelloCoreMvcApp.Models.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloCoreMvcApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly MobileContext db;
        public ProductController(MobileContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Catalog/{catalogName}/{productId:int}")]
        public IActionResult ViewProduct(string catalogName, int productId)
        {
            Item product;

            // Getting an item from db
            //
            if (GetEntityType(catalogName) != null)
            {
                product = (Item)db.Set(GetEntityType(catalogName)).Include(c => (c as Item).Company).FirstOrDefault(p => ((Item)p).Id == productId);
            }
            else return StatusCode(404);

            if (product == null) return StatusCode(404);

            return View(product);

        }

        /// <summary>
        /// Method for searching an entity by name string
        /// </summary>
        /// <returns>Enitity Type</returns>
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
