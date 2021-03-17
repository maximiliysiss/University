using Microsoft.EntityFrameworkCore;
using RockShop.Data;
using RockShop.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockShop.Services
{
    public interface IRockService
    {
        Task<IEnumerable<Rock>> GetRocksAsync();
        Task<Rock> GetRockByIdAsync(long id);
        Task CreateRockAsync(Rock rock);
        Task UpdateRockAsync(Rock rock);
        Task DeleteRockAsync(long id);
        Task<bool> RockExistsAsync(long id);
    }

    public class RockService : IRockService
    {
        private readonly DatabaseContext _context;

        public RockService(DatabaseContext _context)
        {
            this._context = _context;
        }

        public async Task CreateRockAsync(Rock rock)
        {
            _context.Add(rock);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRockAsync(long id)
        {
            var rock = await _context.Rocks.FindAsync(id);
            _context.Rocks.Remove(rock);
            await _context.SaveChangesAsync();
        }

        public async Task<Rock> GetRockByIdAsync(long id) => await _context.Rocks.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<IEnumerable<Rock>> GetRocksAsync() => await _context.Rocks.ToListAsync();

        public async Task<bool> RockExistsAsync(long id) => await _context.Rocks.AnyAsync(e => e.Id == id);

        public async Task UpdateRockAsync(Rock rock)
        {
            var prevValue = await _context.Rocks.AsNoTracking().FirstOrDefaultAsync(x => x.Id == rock.Id);

            if (string.IsNullOrEmpty(rock.Image))
                rock.Image = prevValue.Image;

            _context.Update(rock);
            await _context.SaveChangesAsync();
        }
    }
}
