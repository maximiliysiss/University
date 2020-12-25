using System.ComponentModel.DataAnnotations;

namespace SiteCarAsp.ViewModels.Request
{
    /// <summary>
    /// Запрос на тест драйв
    /// </summary>
    public class CreateTestDriveRequest
    {
        [Required]
        public int CarId { get; set; }
        [Required]
        public string FIO { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
