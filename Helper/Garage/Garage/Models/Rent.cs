using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    public class Rent
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int BoxId { get; set; }
        public Box Box { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; }
    }
}
