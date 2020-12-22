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

    public class EditTripViewModel : BaseTripViewModel, IMapFrom<Trip>, IHaveCustomMappings
    {

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
