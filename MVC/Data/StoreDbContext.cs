using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {}

        public DbSet<Product> Products { get; set; }
    }
}