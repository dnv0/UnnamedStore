using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models.Products;

namespace HelloCoreMvcApp.View
{
    public class Order
    {
        public int OrderId { get; set; }
        public string User { get; set; }
        public string Adress { get; set; }
        public string ContactPhone { get; set; }

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}
