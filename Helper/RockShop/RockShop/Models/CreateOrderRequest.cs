using System.ComponentModel.DataAnnotations;

namespace RockShop.Models
{
    /// <summary>
    /// Модель Создать заказ
    /// </summary>
    public class CreateOrderRequest
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public int OrderCount { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public int OrderId { get; set; }
    }
}
