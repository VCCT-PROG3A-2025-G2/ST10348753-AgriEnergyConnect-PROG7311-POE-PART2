using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AgriEnergyConnect.Models;
using System;

namespace AgriEnergyConnect.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Seed Farmers
            modelBuilder.Entity<Farmer>().HasData(
                new Farmer { Id = 1, Name = "John Mabaso", Email = "john@example.com", Location = "Eastern Cape" },
                new Farmer { Id = 2, Name = "Ayesha Khan", Email = "ayesha@example.com", Location = "Limpopo" }
            );

            // ✅ Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Spinach", Category = "Vegetable", ProductionDate = DateTime.Parse("2024-01-01"), FarmerId = 1 },
                new Product { Id = 2, Name = "Carrots", Category = "Vegetable", ProductionDate = DateTime.Parse("2024-02-15"), FarmerId = 1 },
                new Product { Id = 3, Name = "Apples", Category = "Fruit", ProductionDate = DateTime.Parse("2024-03-01"), FarmerId = 2 }
            );
        }
    }
}
