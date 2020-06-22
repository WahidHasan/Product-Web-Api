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
                .WithMany(c => c.Products);      // from category point of view, one category has many products

            modelBuilder.Entity<Product>()
            .HasOne(p => p.Country)
            .WithMany(c => c.Products);      // from country point of view, one country has many products

            modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, categoryName = "Food" },
            new Category { CategoryId = 2, categoryName = "Cloth" },
            new Category { CategoryId = 3, categoryName = "Medicine" }
        );

            modelBuilder.Entity<Country>().HasData(
                new Country { CountryId = 1, countryName = "Bangladesh" },
                new Country { CountryId = 2, countryName = "Pakistan" },
                new Country { CountryId = 3, countryName = "Germany" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    productName = "Shirt",
                    price = 30,
                    CategoryId = 2,
                    CountryId = 1

                },
                new Product
                {
                    ProductId = 2,
                    productName = "Juice",
                    price = 20,
                    CategoryId = 1,
                    CountryId = 2

                }
            );

        
        }
    }
}
