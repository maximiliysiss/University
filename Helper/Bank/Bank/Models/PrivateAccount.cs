using Bank.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Bank.Models
{
    /// <summary>
    /// ЛС
    /// </summary>
    public class PrivateAccount
    {
        [HideColumn]
        public int Id { get; set; }
        [HideColumn]
        public int CurrencyId { get; set; }
        [DisplayGridName("Валюта")]
        public virtual Currency Currency { get; set; }
        [HideColumn]
        public int UserId { get; set; }
        [DisplayGridName("Клиент")]
        public virtual User User { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        [DisplayGridName("Сумма")]
        public decimal Sum { get; set; }

        public override string ToString() => $"{User.FIO}|{Sum}|{Currency.Name}";

        [NotMapped]
        [HideColumn]
        public string InfoString => $"{Sum}|{Currency.Name}";
    }
}
