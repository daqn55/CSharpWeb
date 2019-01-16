using Microsoft.EntityFrameworkCore;
using MusacaWebApp.Models;

namespace MusacaWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public ApplicationDbContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-FU8DNRQ\SQLEXPRESS;Database=Musaca;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
