using Microsoft.EntityFrameworkCore;
using RockShop.Data.Models;

namespace RockShop.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Rock> Rocks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
