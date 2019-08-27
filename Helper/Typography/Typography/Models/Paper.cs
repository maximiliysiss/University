using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Typography.Models
{
    public class Paper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaperID { get; set; }
        public string PaperName { get; set; }
        public decimal PaperPrice { get; set; }
        public string EditorFIO { get; set; }
        public string EditionCode { get; set; }
        public int PaperQuantity { get; set; }

        public override string ToString() => $"{PaperName}({PaperPrice}|{PaperQuantity})";
    }
}