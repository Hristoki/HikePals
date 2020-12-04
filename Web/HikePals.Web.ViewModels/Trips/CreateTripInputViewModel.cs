namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateTripInputViewModel
    {
        [Required]
        [MaxLength(50)]
        [MinLength(10)]
        public string TripName { get; set; }

        [Required]
        public int CountryId { get; set; }

        public IEnumerable<SelectListItem> CountryItems { get; set; }

        [Required]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> CityItems { get; set; }

        [Required]
        [MaxLength(300)]
        public string Destination { get; set; }

        [Required]
        public int TypeOfDestinationId { get; set; }

        public IEnumerable<SelectListItem> TypeOfDestinationItems { get; set; }

        [Required]
        [MaxLength(500, ErrorMessage = "Description should be between 50 and 500 symbols long")]
        [MinLength(50, ErrorMessage = "Description should be between 50 and 500 symbols long")]
        public string Description { get; set; }

        [Range(2, 20, ErrorMessage = "Group size should be between 2 and 20.")]
        public int MaxGroupSize { get; set; }

        [Required]
        public int TransportId { get; set; }

        public IEnumerable<SelectListItem> TransportItems { get; set; }

        public IFormFile TripImage { get; set; }
    }
}
