﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelloCoreMvcApp.Models.Products;

namespace HelloCoreMvcApp.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
