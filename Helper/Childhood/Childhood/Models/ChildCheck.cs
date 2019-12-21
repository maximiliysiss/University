using Childhood.Extensions.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Childhood.Models
{
    /// <summary>
    /// Состояние ребенка
    /// </summary>
    public enum CheckType
    {
        In,
        Out
    }

    /// <summary>
    /// Состояние ребенка
    /// </summary>
    public class ChildCheck
    {
        [HideColumn]
        public int ID { get; set; }
        [HideColumn]
        public int ChildId { get; set; }
        [DisplayGridName("Ребенок")]
        public virtual Child Child { get; set; }
        [HideColumn]
        public DateTime Date { get; set; } = DateTime.Now;
        [DisplayGridName("Дата и время")]
        [NotMapped]
        public string DateStr => Date.ToString("dd.MM.yyyy HH:mm");
        [HideColumn]
        public CheckType CheckType { get; set; }
        [NotMapped]
        [DisplayGridName("Местонахождение")]
        public string TypeString => CheckType == CheckType.In ? "В садике" : "Не в садике";

        public override string ToString() => $"{Child} / {TypeString} / {DateStr}";
    }
}
