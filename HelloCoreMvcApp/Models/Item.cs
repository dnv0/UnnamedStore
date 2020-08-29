using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloCoreMvcApp.Models
{
    public abstract class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ShortDescription { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
