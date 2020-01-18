using Bank.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Models
{
    /// <summary>
    /// Валюта
    /// </summary>
    public class Currency
    {
        [HideColumn]
        public int Id { get; set; }
        [DisplayGridName("Наименование")]
        public string Name { get; set; }

        public override string ToString() => Name;
    }

    /// <summary>
    /// Конвертация валют
    /// </summary>
    public class ConvertCurrency
    {
        [HideColumn]
        public int Id { get; set; }
        [HideColumn]
        public int CurrencyFromId { get; set; }
        [DisplayGridName("От валюты")]
        public virtual Currency CurrencyFrom { get; set; }
        [HideColumn]
        public int CurrencyToId { get; set; }
        [DisplayGridName("К валюте")]
        public virtual Currency CurrencyTo { get; set; }
        [DisplayGridName("Перевод к (1:1)")]
        public decimal Convert { get; set; }

        public override string ToString() => $"{CurrencyFrom}|{CurrencyTo}|{Convert}";
    }
}
