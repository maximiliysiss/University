using System;
using System.ComponentModel.DataAnnotations;

namespace RockShop.Data.Models
{
    public class Rock
    {
        public long Id { get; set; }
        [Display(Name = "Название")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Дата создания")]
        public DateTimeOffset CreationDateTime { get; set; } = DateTimeOffset.UtcNow;
        [Display(Name = "Изображение")]
        public string Image { get; set; }
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }
}
