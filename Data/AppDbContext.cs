using Microsoft.EntityFrameworkCore;
using SalesTracker.Models;

namespace SalesTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Goal> Goals { get; set; }
    }
}
