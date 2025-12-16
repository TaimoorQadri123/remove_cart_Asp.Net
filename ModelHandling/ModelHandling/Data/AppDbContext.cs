using Microsoft.EntityFrameworkCore;
using ModelHandling.Models;

namespace ModelHandling.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }


        public DbSet<ModelHandling.Models.Students> Students { get; set; }
        public DbSet<ModelHandling.Models.Category> Category { get; set; } = default!;
        public DbSet<ModelHandling.Models.Products> Products { get; internal set; }
        public DbSet<ModelHandling.Models.Cart> Cart { get; internal set; }
    }
}
