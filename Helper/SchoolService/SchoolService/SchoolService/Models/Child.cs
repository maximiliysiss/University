using System;
using System.Collections.Generic;

namespace SchoolService.Models
{
    public class Child
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public DateTime Birthday { get; set; }
        public virtual Class Class { get; set; }
        public virtual List<Mark> Marks { get; set; }
        public bool IsArchive { get; set; }
    }
}