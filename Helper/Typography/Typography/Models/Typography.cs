using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Typography.Models
{
    public class Typography
    {
        [Key]
        [Browsable(false)]
        public int TypographyID { get; set; }
        [DisplayName("Название")]
        public string TypographyName { get; set; }
        [DisplayName("Номер")]
        public string TypographyNumber { get; set; }
        [DisplayName("Адрес")]
        public string TypographyAdress { get; set; }

        public override string ToString() => $"{TypographyName}({TypographyNumber}|{TypographyAdress})";
    }
}
