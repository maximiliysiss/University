using System.ComponentModel.DataAnnotations;

namespace SiteCarAsp.ViewModels.Request
{
    /// <summary>
    /// Запрос на создание кредита
    /// </summary>
    public class CreateCreditRequest
    {
        [Required]
        public int CarId { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
