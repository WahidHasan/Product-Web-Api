using Microsoft.EntityFrameworkCore;
using ProductWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ProductWebApi.Services
{
    public class ProductDbContext :DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Country> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()

                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);       // from category point of view, one category has many products

            modelBuilder.Entity<Product>()
            .HasOne(p => p.Country)
            .WithMany(c => c.Products)
            .HasForeignKey(c => c.CountryId)
            .OnDelete(DeleteBehavior.Cascade);       // from country point of view, one country has many products

          /*  modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Food" },
            new Category { Id = 2, Name = "Cloth" },
            new Category { Id = 3, Name = "Medicine" }
        );

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Bangladesh" },
                new Country { Id = 2, Name = "Pakistan" },
                new Country { Id = 3, Name = "Germany" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Shirt",
                    Price = 30,
                    CategoryId = 2,
                    CountryId = 1

                },
                new Product
                {
                    Id = 2,
                    Name = "Juice",
                    Price = 20,
                    CategoryId = 1,
                    CountryId = 2

                }
            );*/

        
        }
    }
}
