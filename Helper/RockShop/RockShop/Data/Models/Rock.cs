using System;

namespace RockShop.Data.Models
{
    public class Rock
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset CreationDateTime { get; set; } = DateTimeOffset.UtcNow;
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
