using Microsoft.AspNetCore.Mvc;
using RockShop.Models;
using RockShop.Services;
using System.Threading.Tasks;

namespace RockShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        public async Task<IActionResult> CreateOrder([FromForm] CreateOrderRequest createOrderRequest)
        {
            TempData["Message"] = RockShop.Properties.Resource.OrderCreated;
            TempData["MessageType"] = "success";

            if (!await orderService.AddOrderAsync(createOrderRequest.OrderId, createOrderRequest.OrderCount))
            {
                TempData["Message"] = RockShop.Properties.Resource.OrderCreateError;
                TempData["MessageType"] = "error";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
