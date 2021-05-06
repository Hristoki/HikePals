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
        public string Details { get; set; }

        public string UserId { get; set; }

        public bool CurrentUserHasRequestPending => this.Participants.FirstOrDefault(x => x.UserId == this.UserId && x.IsJoinRequestPending) != null ? true : false;

        public bool CurrentUserHasJoinedEvent => this.Participants.FirstOrDefault(x => x.UserId == this.UserId && x.IsJoinRequestPending == false) != null ? true : false;

        public IEnumerable<EventParticipantViewModel> Participants { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(t => t.ImageUrl, s => s.MapFrom(x => "/images/trips/" + x.Trip.Image.Id + x.Trip.Image.Extentsion))
                .ForMember(t => t.CreatedById, s => s.MapFrom(x => x.CreatedById));
        }
    }
}
