using Microsoft.AspNetCore.Mvc.Rendering;
using SiteCarAsp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SiteCarAsp.ViewModels
{
    public class CreditsViewModel
    {
        private readonly IEnumerable<CarInformation> carInformations;

        public CreditsViewModel()
        {
        }

        public CreditsViewModel(IEnumerable<CarInformation> carInformations)
        {
            this.carInformations = carInformations;
        }

        public CreditsViewModel(IEnumerable<CarInformation> carInformations, int carId) : this(carInformations)
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
