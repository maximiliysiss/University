using ClearPluginAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ClearPluginAPI.Services
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ClearAction> ClearActions { get; set; }
    }
}
