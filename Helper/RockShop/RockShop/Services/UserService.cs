using Microsoft.EntityFrameworkCore;
using RockShop.Data;
using RockShop.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockShop.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(long id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(long id);
        Task<bool> UserExistsAsync(long id);
    }

    public class UserService : IUserService
    {
        private readonly ICryptService cryptService;
        private readonly DatabaseContext _context;

        public UserService(ICryptService cryptService, DatabaseContext context)
        {
            this.cryptService = cryptService;
            _context = context;
        }

        public async Task CreateUserAsync(User user)
        {
            user.PasswordHash = cryptService.CreateMD5(user.PasswordHash);

            _context.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(long id) => await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<IEnumerable<User>> GetUsersAsync() => await _context.Users.ToListAsync();

        public async Task UpdateUserAsync(User user)
        {
            user.PasswordHash = cryptService.CreateMD5(user.PasswordHash);

            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(long id) => await _context.Users.AnyAsync(e => e.Id == id);
    }
}
