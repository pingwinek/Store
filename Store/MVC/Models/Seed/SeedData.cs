using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVC.Data;

namespace MVC.Models.Seed
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app) {
            StoreDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StoreDbContext>();

            if (context.Database.GetPendingMigrations().Any()) {
                context.Database.Migrate();
            }

            if (!context.Products.Any()) {
                context.Products.AddRange(
                    new Product {
                        Name = "Butter",
                        Description = "83% fat",
                        Category = "Food",
                        Price = 6.50M
                    },
                    new Product {
                        Name = "Oil",
                        Description = "Kujawski",
                        Category = "Food",
                        Price = 25.15M
                    },
                    new Product {
                        Name = "Maise",
                        Description = "in can",
                        Category = "Food",
                        Price = 3.40M
                    },
                    new Product {
                        Name = "Fish",
                        Description = "frozen",
                        Category = "Food",
                        Price = 16.30M
                    }
                );
                context.SaveChanges();
            }
        }
    }
}