namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Trips;
    using HikePals.Web.ViewModels.ValidationAttributes;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CreateEventInputViewModel : BaseTripViewModel, IMapTo<Event>, IMapFrom<Trip>, IHaveCustomMappings

    {

        public string Details { get; set; }

        public int ApplicationUserId { get; set; }

        [Required]
        [MaxDateEventValidationAttribute(1)]
        public DateTime StartTime { get; set; }

        [Required]
        [MaxDateEventValidationAttribute(1)]
        public DateTime EndTime { get; set; }

        [Required]
        [Range(2, 24)]
        public int MaxGroupSize { get; set; }

        public int TransportId { get; set; }

        public IEnumerable<SelectListItem> TransportItems { get; set; }

        public int TripId { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            //configuration.CreateMap<Trip, CreateEventInputViewModel>()
            //    .ForMember(t => t.TripId, s => s.MapFrom(x => x.Id));

            configuration.CreateMap<CreateEventInputViewModel, Event>()
                .ForMember(t => t.Title, s => s.MapFrom(x => x.Title));
        }
    }
}