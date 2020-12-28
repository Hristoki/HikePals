namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using AutoMapper;
    using HikePals.Common;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditTripViewModel : IMapFrom<Trip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxTripTitleLenght, ErrorMessage = GlobalConstants.TripTitleErrorMessage)]
        [MinLength(GlobalConstants.MinTripTitleLenght, ErrorMessage = GlobalConstants.TripLocationNameErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxLocationNameLenght, ErrorMessage = GlobalConstants.TripLocationNameErrorMessage)]
        [MinLength(GlobalConstants.MinLocationNameLenght, ErrorMessage = GlobalConstants.TripLocationNameErrorMessage)]
        public string LocationName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxTripDescriptionLenght, ErrorMessage = GlobalConstants.TripDescriptionErrorMessage)]
        [MinLength(GlobalConstants.MinTripDescriptionLenght, ErrorMessage = GlobalConstants.TripDescriptionErrorMessage)]
        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int LocationCategoryId { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxLocationNameLenght, ErrorMessage = GlobalConstants.TripLocationNameErrorMessage)]
        [MinLength(GlobalConstants.MinLocationNameLenght, ErrorMessage = GlobalConstants.TripLocationNameErrorMessage)]
        public string LocationCategoryName { get; set; }

        [Required]
        [Range(GlobalConstants.MinTripDistance, GlobalConstants.MaxTripDistance, ErrorMessage = GlobalConstants.TripDistanceErrorMessage)]
        public int Distance { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        public int CityId { get; set; }

        public IEnumerable<SelectListItem> CityItems { get; set; }

        [Required]
        public int TypeOfDestinationId { get; set; }

        public IEnumerable<SelectListItem> CategoriesItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trip, EditTripViewModel>()
                .ForMember(t => t.ImageUrl, s => s.MapFrom(x => x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion))
                .ForMember(t => t.TypeOfDestinationId, s => s.MapFrom(x => x.LocationId))
                .ForMember(t => t.CityId, s => s.MapFrom(x => x.Location.CityId));
        }
    }
}
