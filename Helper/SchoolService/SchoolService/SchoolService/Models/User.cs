﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolService.Models
{
    public enum UserType
    {
        Director, // Директор
        Social, // Соц педагог
        KnowledgeTeacher, // Зауч по учебному процессу
        JobTeacher, // Зауч по рабочему процессу
        Teacher,
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string SecondName { get; set; }
        public DateTime Birthday { get; set; }
        public virtual Document Document { get; set; }
    }
}
