using Microsoft.EntityFrameworkCore;
using RockShop.Data;
using RockShop.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RockShop.Services
{
    /// <summary>
    /// Сервис заказов
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Добавить новый заказ
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderCount"></param>
        /// <returns></returns>
        Task<bool> AddOrderAsync(int orderId, int orderCount);
        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetOrdersAsync();
    }

    public class OrderService : IOrderService
    {
        private readonly DatabaseContext db;

        public OrderService(DatabaseContext databaseContext)
        {
            this.db = databaseContext;
        }

        public async Task<bool> AddOrderAsync(int orderId, int orderCount)
        {
            var rock = await db.Rocks.FirstOrDefaultAsync(x => x.Id == orderId);
            if (rock == null)
                return false;

            db.Add(new Order
            {
                Count = orderCount,
                Name = rock.Name
            });
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync() => await db.Orders.ToListAsync();
    }
}
