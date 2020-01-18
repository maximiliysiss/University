using Bank.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bank.Models
{
    /// <summary>
    /// Операции
    /// </summary>
    public class Operation
    {
        [HideColumn]
        public int Id { get; set; }
        [HideColumn]
        public int PrivateAccountFromId { get; set; }
        [DisplayGridName("От кого")]
        public virtual PrivateAccount PrivateAccountFrom { get; set; }
        [HideColumn]
        public int PrivateAccountToId { get; set; }
        [DisplayGridName("Кому")]
        public virtual PrivateAccount PrivateAccountTo { get; set; }
        [DisplayGridName("Сумма")]
        public float Sum { get; set; }
        [HideColumn]
        public DateTime DateTime { get; set; } = DateTime.Now;
        [NotMapped]
        [DisplayGridName("Дата и время")]
        public string DateTimeString => DateTime.ToString("dd.MM.yyyy HH:mm");

        public override string ToString() => $"{PrivateAccountFrom}|{PrivateAccountTo}|{Sum}|{DateTimeString}";
    }
}
