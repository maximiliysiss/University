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
    public class Distribution
    {
        [Key]
        [Browsable(false)]
        public int DistributionID { get; set; }
        [AttributeGoForm(NextForm = "Paper")]
        [DisplayName("Документ")]
        public virtual Paper Paper { get; set; }
        [AttributeGoForm(NextForm = "PostOfficer")]
        [DisplayName("Обработчик")]
        public virtual PostOfficer PostOfficer { get; set; }
    }
}
