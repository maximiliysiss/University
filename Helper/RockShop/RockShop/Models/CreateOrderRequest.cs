using System.ComponentModel.DataAnnotations;

namespace RockShop.Models
{
    public class CreateOrderRequest
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public int OrderCount { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int OrderId { get; set; }
    }
}
