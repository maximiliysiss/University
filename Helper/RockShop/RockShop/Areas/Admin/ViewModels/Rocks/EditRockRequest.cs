using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace RockShop.Areas.Admin.ViewModels.Rocks
{
    public class EditRockRequest
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime CreationDateTime { get; set; }
        [Required]
        public string CurrentImage { get; set; }
    }
}
