using System.ComponentModel.DataAnnotations;

namespace Typography.Models
{
    public class Paper
    {
        [Key]
        public int PaperID { get; set; }
        public string PaperName { get; set; }
        public decimal PaperPrice { get; set; }
        public string EditorFIO { get; set; }
        public string EditionCode { get; set; }
        public int PaperQuantity { get; set; }
    }
}