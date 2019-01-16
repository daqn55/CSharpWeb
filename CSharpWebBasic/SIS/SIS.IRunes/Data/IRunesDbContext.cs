using Microsoft.EntityFrameworkCore;
using SIS.IRunes.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SIS.IRunes.Data
{
    public class IRunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(true);
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-FU8DNRQ\SQLEXPRESS;Database=IRunes;Integrated Security=True;");
        }
    }
}
