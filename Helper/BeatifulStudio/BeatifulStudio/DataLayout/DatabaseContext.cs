using BeatifulStudio.DataLayout.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatifulStudio.DataLayout
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<UsersService> UsersServices { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-MU02QL6\SQLEXPRESS;Initial Catalog=BeatifulStudio;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UsersService>()
                .HasOne(x => x.User)
                .WithMany(x => x.UsersServices);
            modelBuilder.Entity<UsersService>()
                .HasOne(x => x.Master)
                .WithMany(x => x.MastersService);
        }
    }
}
