using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Typography.Models
{
    public class PostOfficer
    {
        [Key]
        [Browsable(false)]
        public int PostOfficerID { get; set; }
        [DisplayName("Название")]
        public string PostOfficerName { get; set; }
        [DisplayName("Номер")]
        public string PostOfficerNumber { get; set; }
        [DisplayName("Адрес")]
        public string PostOfficerAdress { get; set; }

        public override string ToString() => $"{PostOfficerName}({PostOfficerNumber} | {PostOfficerAdress})";
    }
}