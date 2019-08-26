using System.ComponentModel.DataAnnotations;

namespace Typography.Models
{
    public class PostOfficer
    {
        [Key]
        public int PostOfficerID { get; set; }
        public string PostOfficerName { get; set; }
        public string PostOfficerNumber { get; set; }
        public string PostOfficerAdress { get; set; }
    }
}