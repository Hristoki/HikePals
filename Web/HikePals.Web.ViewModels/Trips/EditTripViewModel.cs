namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class EditTripViewModel : IMapFrom<Trip>, IHaveCustomMappings
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
        public int LocationCategoryId { get; set; }

        [Required]
        public string LocationCategoryName { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Distance { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        [MaxLength(10000, ErrorMessage = "Description should be between 50 and 500 symbols long")]
        [MinLength(25, ErrorMessage = "Description should be between 50 and 500 symbols long")]

        public string Description { get; set; }
        public int Id { get; set; }

        [Required]
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
