namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using HikePals.Common;

    public class CreateTripInputViewModel
    {
        [Required]
        [MaxLength(GlobalConstants.MaxTripTitleLenght, ErrorMessage = GlobalConstants.TripTitleErrorMessage)]
        [MinLength(GlobalConstants.MinTripTitleLenght, ErrorMessage = GlobalConstants.TripTitleErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxLocationNameLenght, ErrorMessage = GlobalConstants.TripLocationNameErrorMessage)]
        [MinLength(GlobalConstants.MinLocationNameLenght, ErrorMessage = GlobalConstants.TripLocationNameErrorMessage)]
        public string LocationName { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Range(GlobalConstants.MinTripDistance, GlobalConstants.MaxTripDistance, ErrorMessage = GlobalConstants.TripDistanceErrorMessage)]
        public int Distance { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxTripDescriptionLenght, ErrorMessage = GlobalConstants.TripDescriptionErrorMessage)]
        [MinLength(GlobalConstants.MinTripDescriptionLenght, ErrorMessage = GlobalConstants.TripDescriptionErrorMessage)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> CategoryItems { get; set; }

        [Required]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> CityItems { get; set; }

        [AllowedImageExtensionsAttribute]
        public IFormFile TripImage { get; set; }
    }
}
