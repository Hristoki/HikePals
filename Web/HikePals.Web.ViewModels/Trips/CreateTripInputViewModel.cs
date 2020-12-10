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

    public class CreateTripInputViewModel: BaseTripViewModel
    {
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
