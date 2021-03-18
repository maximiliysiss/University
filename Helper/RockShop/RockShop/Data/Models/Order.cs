using System;
using System.ComponentModel.DataAnnotations;

namespace RockShop.Data.Models
{
    /// <summary>
    /// Заказ
    /// </summary>
    public class Order
    {
        public long Id { get; set; }
        [Display(Name = "Дата создания")]
        public DateTimeOffset CreationDateTime { get; set; } = DateTimeOffset.UtcNow;
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Количество")]
        public int Count { get; set; }
    }
}
