using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;
using HelloCoreMvcApp.Models.Products;
using HelloCoreMvcApp.Models.Utils;
using HelloCoreMvcApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace HelloCoreMvcApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ProductContext db;

        public CatalogController(ProductContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            // Returns Catalog View
            //
            return View("Catalog");
        }

        [HttpGet]
        [Route("Catalog/{name}")]
        public IActionResult ViewItems(string name)
        {
            // Returns an view model by catalog name
            //
            if (name != null && GetEntityType(name) != null)
            {
                ViewData["Title"] = name;

                // Getting list items from db by category name
                // Sorting and taking first 10 items
                //
                var currentList = db.Set(GetEntityType(name)).Include(x => (x as Item).Company).ToList().ConvertAll(x => (Item)x);
                currentList = currentList.OrderBy(p => p.Price).ToList();
                var items = currentList.Take(10).ToList();
                ViewData["ItemsCount"] = currentList.Count;

                // Storing a category name in cookie
                //
                Response.Cookies.Append("catalogName", name);

                // Initializing a ViewModel
                //
                ViewItemsViewModel viewModel = new ViewItemsViewModel
                {
                    PageViewModel = new PageViewModel(currentList.Count, 1, 10),
                    SortViewModel = new SortViewModel(SortState.PriceAsc),
                    FilterViewModel = new FilterViewModel(null, null),
                    Items = items
                };

                // Passing a list of companies
                //
                ViewData["Companies"] = currentList.Select(c => c.Company).Distinct().ToList();

                return View(viewModel);
            }
            else return StatusCode(404);
        }

        [HttpPost]
        [Route("Catalog/{name}")]
        public IActionResult ViewItems(int[] companiesId, string nameFilter, string cancel, int page, SortState sortOrder = SortState.PriceAsc, int minPrice = 0, int maxPrice = Int32.MaxValue)
        {
            int pageSize = 10;
            List<Item> result = new List<Item>();
            List<Item> currentList = new List<Item>();
            ViewItemsViewModel viewModel;

            // Setting a list of current catalog
            //
            if (Request.Cookies.TryGetValue("catalogName", out string catalogName))
            {
                currentList = db.Set(GetEntityType(catalogName)).Include(x => (x as Item).Company).ToList().ConvertAll(x => (Item)x);
                ViewData["Title"] = catalogName;
                ViewData["ItemsCount"] = currentList.Count;
            }
            else return StatusCode(500);

            // Passing a list of companies
            //
            ViewData["Companies"] = currentList.Select(c => c.Company).Distinct().ToList();

            // Handling the cancel button to return original catalog
            //
            if (!string.IsNullOrEmpty(cancel))
            {
                var modelItems = currentList.Take(10).ToList();
                return View(viewModel = new ViewItemsViewModel
                {
                    PageViewModel = new PageViewModel(currentList.Count, 1, pageSize),
                    SortViewModel = new SortViewModel(SortState.PriceAsc),
                    FilterViewModel = new FilterViewModel(null, null, 0, Int32.MaxValue),
                    Items = modelItems
                });
            }

            // Handling the search button
            //
            if (!String.IsNullOrEmpty(nameFilter))
            {
                // Filtering by name
                //
                currentList = currentList.Where(n => n.Name.Contains(nameFilter)).ToList();

                // Sorting
                //
                switch (sortOrder)
                {
                    case SortState.PriceAsc:
                        currentList = currentList.OrderBy(s => s.Price).ToList();
                        break;
                    case SortState.PriceDesc:
                        currentList = currentList.OrderByDescending(s => s.Price).ToList();
                        break;
                }

                // Taking first 10 items and passing
                //
                var modelItems = currentList.Take(10).ToList();
                return View(viewModel = new ViewItemsViewModel
                {
                    PageViewModel = new PageViewModel(currentList.Count, 1, pageSize),
                    SortViewModel = new SortViewModel(sortOrder),
                    FilterViewModel = new FilterViewModel(null, nameFilter),
                    Items = modelItems
                });
            }

            // Filter by cmopanies, prices, and sorting
            //
            result = GetFilterSortList(currentList ,companiesId, sortOrder, minPrice, maxPrice);

            // Pagination
            //
            int count = result.Count;
            var items = result.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Initiizing a ViewModel
            //
            viewModel = new ViewItemsViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(companiesId, nameFilter, minPrice, maxPrice),
                Items = items
            };

            return View(viewModel);
        }

        /// <summary>
        /// Method for searching an entity by name string
        /// </summary>
        /// <returns>Enitity Type</returns>
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

        /// <summary>
        /// Method for sorting and filtering
        /// </summary>
        /// <returns>Finihed list</returns>
        private List<Item> GetFilterSortList(List<Item> currentList,int[] companiesId, SortState sortOrder, int minPrice, int maxPrice)
        {
            List<Item> result = new List<Item>();

            // Filter by companies array
            //
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

            // Sorting
            //
            switch (sortOrder)
            {
                case SortState.PriceAsc:
                    result = result.OrderBy(s => s.Price).ToList();
                    break;
                case SortState.PriceDesc:
                    result = result.OrderByDescending(s => s.Price).ToList();
                    break;
            }

            // Filter by price
            //
            foreach (var item in result.ToList())
            {
                if (item.Price < minPrice || item.Price > maxPrice)
                {
                    result.Remove(item);
                }
            }

            return result;
        }
    }
}
