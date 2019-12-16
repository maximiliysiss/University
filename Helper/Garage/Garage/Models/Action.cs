using Garage.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Garage.Models
{
    public enum ActionType
    {
        In,
        Out
    }

    /// <summary>
    /// Действие
    /// </summary>
    public class Action
    {
        [HideColumn]
        public int ID { get; set; }
        [HideColumn]
        public ActionType ActionType { get; set; }
        [HideColumn]
        public int RentId { get; set; }
        [DisplayGridName("Аренда")]
        public virtual Rent Rent { get; set; }
        [HideColumn]
        public DateTime DateTime { get; set; } = DateTime.Now;
        [DisplayGridName("Время и дата")]
        [NotMapped]
        public string DateTimeStr => DateTime.ToString("dd.MM.yyyy HH:mm");
        [NotMapped]
        [DisplayGridName("Тип")]
        public string ActionTypeStr => ActionType == ActionType.In ? "Въезд" : "Выезд";

        public override string ToString() => $"{ActionTypeStr} / {DateTimeStr} / {Rent}";
    }
}
