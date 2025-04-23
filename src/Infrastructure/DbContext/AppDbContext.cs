using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DbContext 
{
    public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}