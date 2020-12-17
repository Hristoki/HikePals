namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Users;

    public class EventViewModel : BaseEventViewModel, IMapFrom<Event>, IHaveCustomMappings
    {
        public string Image { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                //.ForMember(t => t.TripId, s => s.MapFrom(x => x.Id))
                .ForMember(t => t.Image, s => s.MapFrom(x => "/images/trips/" + x.Trip.Image.Id + x.Trip.Image.Extentsion));
                //.ForMember(t => t.TripDuration, s => s.MapFrom(x => x.Trip.Duration))
                //.ForMember(t => t.TripTitle, s => s.MapFrom(x => x.Trip.Title));
        }
    }
}
