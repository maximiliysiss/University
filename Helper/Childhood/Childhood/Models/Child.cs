using Childhood.Extensions.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Childhood.Models
{
    public class Child
    {
        [HideColumn]
        public int ID { get; set; }
        [DisplayGridName("ФИО")]
        public string FIO { get; set; }
        [HideColumn]
        public DateTime Birthday { get; set; } = DateTime.Today;
        [NotMapped]
        [DisplayGridName("День рождения")]
        public string BirthDayStr => Birthday.ToString("dd.MM.yyyy");
        [DisplayGridName("Адрес")]
        public string Address { get; set; }
        [HideColumn]
        public int GroupId { get; set; }
        [DisplayGridName("Группа")]
        public virtual Group Group { get; set; }
        [HideColumn]
        public int? MomId { get; set; }
        [DisplayGridName("Мама")]
        public virtual User Mom { get; set; }
        [HideColumn]
        public int? DaddyId { get; set; }
        [DisplayGridName("Папа")]
        public virtual User Daddy { get; set; }

        public override string ToString() => $"{FIO} / {BirthDayStr}";
    }
}
