namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;
    using HikePals.Common;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateTripInputViewModel : IMapFrom<Trip>, IHaveCustomMappings
    {
        public int Id { get; set; }

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
        [Display(Name = "Image")]
        public IFormFile TripImage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration
                .CreateMap<Trip, CreateTripInputViewModel>()
                .ForMember(t => t.CategoryId, s => s.MapFrom(x => x.Location.CategoryId))
                .ForMember(t => t.CityId, s => s.MapFrom(x => x.Location.CityId));
        }
    }
}
