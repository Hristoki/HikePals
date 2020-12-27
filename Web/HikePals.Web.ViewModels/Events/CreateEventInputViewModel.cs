namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using HikePals.Common;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Trips;
    using HikePals.Web.ViewModels.ValidationAttributes;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateEventInputViewModel : TripViewModel, IMapTo<Event>, IHaveCustomMappings

    {
        public int TripId { get; set; }

        [Required]
        [MaxLength(GlobalConstants.MaxTripDescriptionLenght, ErrorMessage = GlobalConstants.TripDescriptionErrorMessage)]
        [MinLength(GlobalConstants.MinTripDescriptionLenght, ErrorMessage = GlobalConstants.TripDescriptionErrorMessage)]
        public string Details { get; set; }

        public int ApplicationUserId { get; set; }

        [Required]
        [MaxDateEventValidationAttribute(1)]
        public DateTime StartTime { get; set; }

        [Required]
        [MaxDateEventValidationAttribute(1)]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(GlobalConstants.MinGorupSize, GlobalConstants.MaxGroupSize, ErrorMessage = GlobalConstants.GroupsSizeErrorMessage)]
        public int MaxGroupSize { get; set; }

        [Required]
        public int TransportId { get; set; }

        public IEnumerable<SelectListItem> TransportItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Trip, CreateEventInputViewModel>()
                .ForMember(t => t.TripId, s => s.MapFrom(x => x.Id));

            configuration.CreateMap<CreateEventInputViewModel, Event>()
                .ForMember(t => t.Title, s => s.MapFrom(x => x.Title));

            configuration.CreateMap<CreateEventInputViewModel, Event>()
                .ForMember(t => t.Title, s => s.MapFrom(x => x.Title));

            configuration.CreateMap<Event, EditEventViewModel>()
                .ForMember(x => x.ImageUrl, s => s.MapFrom(x => x.Trip.Image == null ? "No image available" : "/images/trips/" + x.Trip.Image.Id + x.Trip.Image.Extentsion));
        }
    }
}