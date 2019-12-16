﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Production.Extensions.Attributes
{
    /// <summary>
    /// Аттрибут для скрытия колонки
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class HideColumnIfAutoGenerated : System.Attribute
    {
        public HideColumnIfAutoGenerated()
        {
        }
    }
}