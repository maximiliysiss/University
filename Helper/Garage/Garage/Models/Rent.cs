using Garage.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Garage.Models
{
    /// <summary>
    /// Аренда
    /// </summary>
    public class Rent
    {
        [HideColumn]
        public int ID { get; set; }
        [HideColumn]
        public int UserId { get; set; }
        [DisplayGridName("Пользователь")]
        public virtual User User { get; set; }
        [HideColumn]
        public int BoxId { get; set; }
        [DisplayGridName("Бокс")]
        public virtual Box Box { get; set; }
        [HideColumn]
        public DateTime StartDate { get; set; } = DateTime.Today;
        [HideColumn]
        public DateTime? EndDate { get; set; }
        [NotMapped]
        [DisplayGridName("Дата начала")]
        public string StartDateStr => StartDate.ToString("dd.MM.yyyy HH:mm");
        [NotMapped]
        [DisplayGridName("Дата окончания")]
        public string EndDateStr => EndDate?.ToString("dd.MM.yyyy HH:mm");

        public override string ToString() => $"{User} / {Box} / {StartDate} / {EndDate}";
    }
}
