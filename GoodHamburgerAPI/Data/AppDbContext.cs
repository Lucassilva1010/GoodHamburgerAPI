using Microsoft.EntityFrameworkCore;
using GoodHamburgerAPI.Models;

namespace GoodHamburgerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "X Burger", Price = 5.00m, Category = "Sandwich" },
                new Product { Id = 2, Name = "X Egg", Price = 4.50m, Category = "Sandwich" },
                new Product { Id = 3, Name = "X Bacon", Price = 7.00m, Category = "Sandwich" },
                new Product { Id = 4, Name = "Fries", Price = 2.00m, Category = "Extra" },
                new Product { Id = 5, Name = "Soft drink", Price = 2.50m, Category = "Extra" }
            );
        }
    }
}