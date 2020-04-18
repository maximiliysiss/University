﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslateChatter.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string UICode { get; set; }
    }
}
