using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Typography.Models;

namespace Typography.Services
{
    public interface IDatabaseContext
    {
        DbSet<Paper> Papers { get; set; }
        DbSet<Distribution> Distributions { get; set; }
        DbSet<PostOfficer> PostOfficers { get; set; }
        DbSet<Release> Releases { get; set; }
        DbSet<Models.Typography> Typographies { get; set; }

        int SaveChanges();
        EntityEntry Add(object o);
        EntityEntry Update(object obj);
        EntityEntry Remove(object obj);
    }

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(string connectionString)
        {
            ConnectionString = new SqlConnectionStringBuilder(connectionString);
        }

        public SqlConnectionStringBuilder ConnectionString { get; private set; }

        public DbSet<Paper> Papers { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<PostOfficer> PostOfficers { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Models.Typography> Typographies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString.ConnectionString).UseLazyLoadingProxies();
        }
    }
}
