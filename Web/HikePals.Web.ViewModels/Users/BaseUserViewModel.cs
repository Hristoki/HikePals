namespace HikePals.Web.ViewModels.Users
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public abstract class BaseUserViewModel : IMapFrom<EventsUsers>
    {
        public string Name { get; set; }

        public string CityName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string UserId { get; set; }

        public int Age => DateTime.UtcNow.Year - this.DateOfBirth.Year;

        public bool IsJoinRequestPending { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<EventsUsers, EventParticipantViewModel>()
                .ForMember(t => t.Name, s => s.MapFrom(x => x.User.UserName))
                .ForMember(t => t.CityName, s => s.MapFrom(x => x.User.City.Name))
                .ForMember(t => t.DateOfBirth, s => s.MapFrom(x => x.User.DateOfBirth))
                .ForMember(t => t.IsJoinRequestPending, s => s.MapFrom(x => x.PendingJoinRequest));
        }
    }
}
