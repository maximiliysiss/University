using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace RockShop.Areas.Admin.ViewModels.Rocks
{
    public class CreateRockRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
