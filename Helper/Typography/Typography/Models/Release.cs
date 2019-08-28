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
        [DisplayName("Типография")]
        [AttributeGoForm(NextForm = "Typography")]
        public virtual Typography Typography { get; set; }
        [DisplayName("Документ")]
        [AttributeGoForm(NextForm = "Paper")]
        public virtual Paper Paper { get; set; }
    }
}
