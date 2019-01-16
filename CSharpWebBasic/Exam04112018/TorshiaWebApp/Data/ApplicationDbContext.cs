using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TorshiaWebApp.Models;

namespace TorshiaWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<UserTask> UsersTasks { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<TaskSector> TasksSectors { get; set; }

        public ApplicationDbContext() {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-FU8DNRQ\SQLEXPRESS;Database=Torshia;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
