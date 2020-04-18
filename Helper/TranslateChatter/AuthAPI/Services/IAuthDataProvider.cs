using AuthAPI.Models.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Services
{
    public interface IAuthDataProvider
    {
        IQueryable<User> Users { get; }
        IQueryable<Role> Roles { get; }
        void Add<T>(T obj);
        void Delete<T>(T obj);
        void Update<T>(T obj);
        void SaveChanges();
        Task SaveChangesAsync();
    }

    public class AuthDataProvider : DbContext, IAuthDataProvider
    {
        public AuthDataProvider(DbContextOptions options) : base(options)
        {
        }

        void IAuthDataProvider.Add<T>(T obj) => this.Add(obj);
        public void Delete<T>(T obj) => base.Remove(obj);
        void IAuthDataProvider.SaveChanges() => base.SaveChanges();
        public Task SaveChangesAsync() => base.SaveChangesAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Entity<User>().Property(x => x.Email).HasMaxLength(400);

            modelBuilder.Entity<Role>().HasIndex(x => x.Name);
            modelBuilder.Entity<User>().HasIndex(x => x.Email);

            modelBuilder.Entity<Language>().HasData(new[] {
                new Language{ Id = 1, Name = "English", Code = "en", UICode = "en-US" },
                new Language{ Id = 2, Name = "Русский", Code = "ru", UICode = "ru-RU" }
            });
        }

        void IAuthDataProvider.Update<T>(T obj) => base.Update(obj);

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        IQueryable<User> IAuthDataProvider.Users => this.Users;
        IQueryable<Role> IAuthDataProvider.Roles => this.Roles;
    }
}
