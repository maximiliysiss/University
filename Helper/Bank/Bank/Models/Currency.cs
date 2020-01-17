using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ConvertCurrency
    {
        public int Id { get; set; }
        public int CurrencyFromId { get; set; }
        public Currency CurrencyFrom { get; set; }
        public int CurrencyToId { get; set; }
        public Currency CurrencyTo { get; set; }
    }
}
