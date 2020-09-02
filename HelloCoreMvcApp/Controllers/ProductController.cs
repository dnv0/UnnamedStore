using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;
using HelloCoreMvcApp.Models.Utils;
using Microsoft.AspNetCore.Mvc;

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

        [Route("products/{catalogName}/{productId:int}")]
        public IActionResult ViewProduct(string catalogName, int productId)
        {
            if(String.IsNullOrEmpty(catalogName) || productId == null)
            {
                return Content("Product not found");
            }

            // Setting a list of current catalog
            //
            if (GetEntityType(catalogName) != null)
            {
                Item product = (Item)db.Set(GetEntityType(catalogName)).FirstOrDefault(p => (p as Item).Id == productId);
            }
            else return StatusCode(404);

            return Content("Item id: {product.Id} | Product Name: {product.Name}");
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
