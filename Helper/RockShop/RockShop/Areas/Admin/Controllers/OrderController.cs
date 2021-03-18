using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RockShop.Services;
using System.Threading.Tasks;

namespace RockShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> Index() => View(await orderService.GetOrdersAsync());
    }
}
