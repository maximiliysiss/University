using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    public class Box
    {
        public int ID { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
