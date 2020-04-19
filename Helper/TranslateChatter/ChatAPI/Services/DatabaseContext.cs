using ChatAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ChatAPI.Services
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
    }
}
