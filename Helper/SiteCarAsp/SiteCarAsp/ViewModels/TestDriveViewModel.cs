using Microsoft.AspNetCore.Mvc.Rendering;
using SiteCarAsp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SiteCarAsp.ViewModels
{
    public class TestDriveViewModel
    {
        private readonly IEnumerable<CarInformation> carInformations;

        public TestDriveViewModel()
        {
        }

        public TestDriveViewModel(IEnumerable<CarInformation> carInformations)
        {
            this.carInformations = carInformations;
        }

        public TestDriveViewModel(IEnumerable<CarInformation> carInformations, int carId) : this(carInformations)
        {
            CarId = carId;
        }

        [Required]
        public int CarId { get; set; }
        [Required]
        public string Fio { get; set; }
        [Required]
        public string Phone { get; set; }

        public SelectList Cars => new SelectList(carInformations ?? Enumerable.Empty<CarInformation>(), "Id", "Name");

    }
}
