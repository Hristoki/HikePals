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

    public class CreateTripInputViewModel
    {
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        public string Title { get; set; }

        [Required]
        [MaxLength(300)]
        [MinLength(3)]
        public string LocationName { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Distance { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "Description should be between 25 and 10000 symbols long")]
        [MinLength(25, ErrorMessage = "Description should be between 25 and 10000 symbols long")]
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
