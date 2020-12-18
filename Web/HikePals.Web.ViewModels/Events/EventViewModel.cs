namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Users;

    public class EventViewModel : BaseEventViewModel, IMapFrom<Event>, IHaveCustomMappings
    {
        public string Image { get; set; }

        public string CreatedBy { get; set; }

        public bool HasJoinedEvent => this.Participants.FirstOrDefault(x => x.UserId == this.UserId) == null ? false : true;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(t => t.Image, s => s.MapFrom(x => "/images/trips/" + x.Trip.Image.Id + x.Trip.Image.Extentsion))
                .ForMember(t => t.CreatedBy, s => s.MapFrom(x => x.CreatedById));
        }
    }
}
