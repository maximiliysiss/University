using ClearPluginAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearPluginAPI.Services
{
    /// <summary>
    /// БД
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Таблица
        /// </summary>
        public DbSet<ClearAction> ClearActions { get; set; }
    }
}
