using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Typography.Models
{
    public class Paper
    {
        [Key]
        [Browsable(false)]
        public int PaperID { get; set; }
        [DisplayName("Название")]
        public string PaperName { get; set; }
        [DisplayName("Цена")]
        public decimal PaperPrice { get; set; }
        [DisplayName("ФИО работника")]
        public string EditorFIO { get; set; }
        [DisplayName("Код изменения")]
        public string EditionCode { get; set; }
        [DisplayName("Количество страниц")]
        public int PaperQuantity { get; set; }

        public override string ToString() => $"{PaperName}({PaperPrice}|{PaperQuantity})";
    }
}