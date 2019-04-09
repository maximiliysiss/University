using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatifulStudio.DataLayout.Models
{
    public enum ServiceType
    {
        BarberShop,
        Manicure,
        Massage
    }

    public class Service
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ServiceType ServiceType { get; set; }
        public double Price { get; set; }
    }
}
