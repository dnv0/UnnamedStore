using Microsoft.EntityFrameworkCore;
using HelloCoreMvcApp.Models.Products;
using HelloCoreMvcApp.Models.OrderAggregate;

namespace HelloCoreMvcApp.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Ship> Ships { get; set; }
        public DbSet<ToiletPaper> ToiletPapers { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ProductContext(DbContextOptions<ProductContext> options)
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
