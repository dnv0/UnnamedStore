using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloCoreMvcApp.Models;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelloCoreMvcApp.Models.OrderAggregate
{
    public class Order
    {
        public int OrderId { get; set; }
        public string User { get; set; }
        public string Address { get; set; }
        public string ContactPhone { get; set; }
        public bool DeliveryRequired { get; set; }

        // TODO: Make EF to store list of items
        // public virtual List<int> itemIds { get; set; }
    }
}
