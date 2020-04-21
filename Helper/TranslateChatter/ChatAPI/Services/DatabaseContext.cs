using ChatAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChatAPI.Services
{
    public interface IDatabaseContext
    {
        IQueryable<Room> Rooms { get; }
        void ApplyChanges();
        void Add(object obj);
        Task SaveChangesAsync();
        void Remove(object obj);
        void Update(object obj);
    }

    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DbSet<Room> Rooms { get; set; }

        IQueryable<Room> IDatabaseContext.Rooms => this.Rooms;

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public void ApplyChanges()
        {
            Database.Migrate();
        }

        void IDatabaseContext.Add(object obj) => this.Add(obj);
        public Task SaveChangesAsync() => this.SaveChangesAsync();
        void IDatabaseContext.Remove(object obj) => this.Remove(obj);
        void IDatabaseContext.Update(object obj) => this.Update(obj);
    }
}
