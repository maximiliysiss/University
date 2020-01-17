using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Models
{
    public class Operation
    {
        public int Id { get; set; }
        public int PrivateAccountFromId;
        public PrivateAccount PrivateAccountFrom { get; set; }
        public int PrivateAccountToId;
        public PrivateAccount PrivateAccountTo { get; set; }
        public float Sum { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
