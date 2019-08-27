using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Typography.Models
{
    public class PostOfficer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostOfficerID { get; set; }
        public string PostOfficerName { get; set; }
        public string PostOfficerNumber { get; set; }
        public string PostOfficerAdress { get; set; }

        public override string ToString() => $"{PostOfficerName}({PostOfficerNumber} | {PostOfficerAdress})";
    }
}