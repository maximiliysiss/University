using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Models
{
    public enum ActionType
    {
        In,
        Out
    }

    public class Action
    {
        public int ID { get; set; }
        public ActionType ActionType { get; set; }
        public int RentId { get; set; }
        public Rent Rent { get; set; }
        public DateTime DateTime { get; set; }
    }
}
