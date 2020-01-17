using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Models
{
    public class PrivateAccount
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public float Sum { get; set; }
    }
}
