using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Typography.Services;
namespace Typography.Models
{
    public class Release
    {
        [Key]
        [Browsable(false)]
        public int ReleaseID { get; set; }
        [Browsable(false)]
        [AttributeGoForm(NextForm = "Typography")]
        public virtual Typography Typography { get; set; }
        [Browsable(false)]
        [AttributeGoForm(NextForm = "Paper")]
        public virtual Paper Paper { get; set; }

        [DisplayName("Типография")]
        public string TypographyString => Typography.ToString();

        [DisplayName("Документ")]
        public string PaperStr => Paper.ToString();
    }
}
