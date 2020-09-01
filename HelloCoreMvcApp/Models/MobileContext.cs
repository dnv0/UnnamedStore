using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HelloCoreMvcApp.Models.Products;
using Microsoft.AspNetCore.Http.Features;

namespace HelloCoreMvcApp.Models
{
    public class MobileContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<Company> Companies { get; set; }

        public MobileContext(DbContextOptions<MobileContext> options)
            :base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Items");
        }
    }
}
