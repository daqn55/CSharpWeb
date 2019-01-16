using Microsoft.EntityFrameworkCore;
using PandaWebApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<User> Users { get; set; }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-FU8DNRQ\SQLEXPRESS;Database=Panda;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(x => x.Packages).WithOne(x => x.Recipient).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>().HasMany(x => x.Receipts).WithOne(x => x.Recipient).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Package>().HasOne(x => x.Receipt).WithOne(x => x.Package).HasForeignKey<Receipt>(x => x.PackageId);
        }
    }
}
