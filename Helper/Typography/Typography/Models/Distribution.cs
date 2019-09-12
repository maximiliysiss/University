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
        [Browsable(false)]
        public virtual Paper Paper { get; set; }
        [AttributeGoForm(NextForm = "PostOfficer")]
        [Browsable(false)]
        public virtual PostOfficer PostOfficer { get; set; }

        [DisplayName("Обработчик")]
        public string PostOfficerStr => PostOfficer.ToString();
        [DisplayName("Документ")]
        public string PaperStr => Paper.ToString();
    }
}
