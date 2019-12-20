using System;
using System.Collections.Generic;
using System.Text;

namespace Childhood.Models
{
    public enum CheckType
    {
        In,
        Out
    }

    public class ChildCheck
    {
        public int ID { get; set; }
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public CheckType CheckType { get; set; }
    }
}
