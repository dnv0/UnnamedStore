using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;
using HelloCoreMvcApp.Models.Products;
using HelloCoreMvcApp.Models.Utils;
using HelloCoreMvcApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HelloCoreMvcApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly MobileContext db;
        public CatalogController(MobileContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View("Catalog");
        }

        [HttpGet]
        public IActionResult ViewItems(string name)
        {
            
            if (name != null)
            {
                if (GetEntityType(name) != null)
                {

                    var target = db.Set(GetEntityType(name)).ToList().ConvertAll(x => (IItem)x);
                    ViewData["Title"] = name;
                    ViewData["Count"] = target.Count;
                    return View(target);
                }
                else return StatusCode(404);
            }
            else return StatusCode(404);

        }

        private Type GetEntityType(string name)
        {
            var entityTypes = db.Model.GetEntityTypes().Select(t => t.ClrType).ToList();
            foreach(var item in entityTypes)
            {
                if (item.Name.ToString() == name)
                    return item;
            }

            return null;
        }
    }
}
