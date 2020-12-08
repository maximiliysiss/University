using System.ComponentModel.DataAnnotations;

namespace SiteCarAsp.ViewModels.Request
{
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
