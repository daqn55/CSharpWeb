using ChushkaPrepExam.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChushkaPrepExam.Data
{
    public class ChushkaDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public ChushkaDbContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-FU8DNRQ\SQLEXPRESS;Database=Chushka;Integrated Security=True;");
        }
    }
}
