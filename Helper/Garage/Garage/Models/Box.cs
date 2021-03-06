﻿using Garage.Extensions.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Garage.Models
{
    /// <summary>
    /// Бокс
    /// </summary>
    public class Box
    {
        [HideColumn]
        public int ID { get; set; }
        [DisplayGridName("Ширина")]
        public double Width { get; set; }
        [DisplayGridName("Высота")]
        public double Height { get; set; }
        [DisplayGridName("Адрес")]
        public string Address { get; set; }
        [DisplayGridName("Цена")]
        public double Price { get; set; }
        [HideColumn]
        public virtual List<Rent> Rents { get; set; }
        [NotMapped]
        [DisplayGridName("Аренда")]
        public Rent Rent => Rents?.FirstOrDefault(x => x.EndDate == null);

        public override string ToString() => $"{Width}*{Height} / {Address} / {Price}";
    }
}
