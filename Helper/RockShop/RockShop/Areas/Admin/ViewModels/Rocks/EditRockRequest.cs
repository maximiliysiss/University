using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace RockShop.Areas.Admin.ViewModels.Rocks
{
    public class EditRockRequest
    {
        [Required(ErrorMessage = "Обязательное поле")]
        public long Id { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public DateTime CreationDateTime { get; set; }
        [Required(ErrorMessage = "Обязательное поле")]
        public string CurrentImage { get; set; }
    }
}
