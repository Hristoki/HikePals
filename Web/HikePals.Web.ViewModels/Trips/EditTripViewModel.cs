namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditTripViewModel : BaseTripViewModel
    {

        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string TripName { get; set; }

        [Required]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> CityItems { get; set; }

        [Required]
        [MaxLength(300)]
        public string Destination { get; set; }

        public int Distance { get; set; }

        public TimeSpan Duration { get; set; }

        [Required]
        public int TypeOfDestinationId { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "Description should be between 50 and 500 symbols long")]
        [MinLength(25, ErrorMessage = "Description should be between 50 and 500 symbols long")]

        public string Description { get; set; }
    }
}
