using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Models
{
    public class Typography
    {
        [Key]
        public int TypographyID { get; set; }
        public string TypographyName { get; set; }
        public string TypographyNumber { get; set; }
        public string TypographyAdress { get; set; }

        public override string ToString() => $"{TypographyName}({TypographyNumber}|{TypographyAdress})";
    }
}
