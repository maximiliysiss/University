using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RockShop.Areas.Admin.ViewModels.Rocks
{
    /// <summary>
    /// Модель создания камня
    /// </summary>
    public class CreateRockRequest
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public decimal Price { get; set; }
    }
}
