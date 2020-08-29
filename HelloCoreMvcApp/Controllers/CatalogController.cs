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
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;

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
            // Returns an view model by catalog name
            //
            if (name != null && GetEntityType(name) != null)
            {
                ViewData["Title"] = name;

                var currentList = db.Set(GetEntityType(name)).Include(x => (x as Item).Company).ToList().ConvertAll(x => (Item)x);
                var items = currentList.Take(10).ToList();

                Response.Cookies.Append("catalogName", name);

                ViewItemsViewModel viewModel = new ViewItemsViewModel
                {
                    PageViewModel = new PageViewModel(currentList.Count, 1, 10),
                    SortViewModel = new SortViewModel(SortState.PriceAsc),
                    FilterViewModel = new FilterViewModel(null, null),
                    Items = items
                };

                ViewData["Companies"] = db.Companies.ToList();

                return View(viewModel);
            }
            else return StatusCode(404);
        }

        [HttpPost]
        public IActionResult ViewItems(int[] companiesId, string name, string cancel, int page, SortState sortOrder = SortState.PriceAsc, int minPrice = 0, int maxPrice = 999999)
        {
            int pageSize = 10;
            List<Item> result = new List<Item>();
            List<Item> currentList = new List<Item>();
            ViewData["Companies"] = db.Companies.ToList();

            // Setting a list of current catalog
            //
            if (Request.Cookies.TryGetValue("catalogName", out string catalogName))
            {
                currentList = db.Set(GetEntityType(catalogName)).Include(x => (x as Item).Company).ToList().ConvertAll(x => (Item)x);
                ViewData["Title"] = catalogName;
            }
            else return StatusCode(500);

            // Handling the cancel button to return original catalog
            //
            if (!string.IsNullOrEmpty(cancel))
            {
                var sourceItems = currentList.Take(10).ToList();
                return View(new ViewItemsViewModel
                {
                    PageViewModel = new PageViewModel(currentList.Count, 1, 10),
                    SortViewModel = new SortViewModel(SortState.PriceAsc),
                    FilterViewModel = new FilterViewModel(null, null),
                    Items = sourceItems
                });
            }

            // Filter by companies array
            //
            companiesId = companiesId.Distinct().ToArray();

            if (companiesId.Length != 0)
            {
                for (int i = 0; i < companiesId.Length; i++)
                {
                    result.AddRange(currentList.FindAll(
                        delegate (Item item)
                        {
                            return item.Company.Id == companiesId[i];
                        }));
                }
            }
            else result = currentList.ToList();

            // Setting default values
            //

            // Filter by price
            //
            foreach (var item in result.ToList())
            {
                if (item.Price < minPrice || item.Price > maxPrice)
                {
                    result.Remove(item);
                }
            }

            int count = result.Count;
            var items = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewItemsViewModel viewModel = new ViewItemsViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(companiesId, name, minPrice, maxPrice),
                Items = items
            };

            return View(viewModel);
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
