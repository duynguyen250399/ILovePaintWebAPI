using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Provider> Providers { get; set; }
    }
}
