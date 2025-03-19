 using Microsoft.EntityFrameworkCore;
using YourFavECommerce.Api.Models;

namespace YourFavECommerce.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
    }
}
