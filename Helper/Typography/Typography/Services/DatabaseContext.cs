using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Typography.Models;

namespace Typography.Services
{
    /// <summary>
    /// Интерфейс для подключения к БД
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Таблицы
        /// </summary>
        DbSet<Paper> Papers { get; set; }
        DbSet<Distribution> Distributions { get; set; }
        DbSet<PostOfficer> PostOfficers { get; set; }
        DbSet<Release> Releases { get; set; }
        DbSet<Models.Typography> Typographies { get; set; }

        /// <summary>
        /// Сохранить
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// Добавить
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        EntityEntry Add(object o);
        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        EntityEntry Update(object obj);
        /// <summary>
        /// Удалить
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        EntityEntry Remove(object obj);
    }

    /// <summary>
    /// Реализация подключения к БД
    /// </summary>
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(string connectionString)
        {
            ConnectionString = new SqlConnectionStringBuilder(connectionString);
        }

        /// <summary>
        /// Строка подключения
        /// </summary>
        public SqlConnectionStringBuilder ConnectionString { get; private set; }

        /// <summary>
        /// Таблицы
        /// </summary>
        public DbSet<Paper> Papers { get; set; }
        public DbSet<Distribution> Distributions { get; set; }
        public DbSet<PostOfficer> PostOfficers { get; set; }
        public DbSet<Release> Releases { get; set; }
        public DbSet<Models.Typography> Typographies { get; set; }

        /// <summary>
        /// Подключение к БД
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(ConnectionString.ConnectionString).UseLazyLoadingProxies();
        }
    }
}
