using HelloCoreMvcApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCoreMvcApp.ViewModels
{
    public class FilterViewModel
    {
        public FilterViewModel( int[] companies, string name, int? minPrice = 0, int? maxPrice = Int32.MaxValue)
        {
            SelectedCompanies = companies;
            SelectedName = name;
            SelectedMinPrice = minPrice;
            SelectedMaxPrice = maxPrice;
        }

        public int[] SelectedCompanies { get; private set; }
        public string SelectedName { get; private set; }
        public int? SelectedMinPrice { get; private set; }
        public int? SelectedMaxPrice { get; private set; }
    }
}
