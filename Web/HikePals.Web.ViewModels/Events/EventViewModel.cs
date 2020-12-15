using AutoMapper;
using HikePals.Data.Models;
using HikePals.Web.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace HikePals.Web.ViewModels.Events
{
    public class EventViewModel
    {

        public int Id { get; set; }

        public string TripName { get; set; }

        public string Details { get; set; }

        public int ApplicationUserId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int MaxGroupSize { get; set; }

        public string Transport { get; set; }

        public int TripId { get; set; }

        public string TripImageUrl { get; set; }

        public int TripDuration { get; set; }

        public int TripDistance { get; set; }

        public IEnumerable<UserViewModel> Participants { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Event, EventViewModel>()
                .ForMember(t => t.TripId, s => s.MapFrom(x => x.Id))
                .ForMember(t => t.TripImageUrl, s => s.MapFrom(x => x.Trip.Image == null ? "No image available" : "/images/trips/" + x.Trip.Image.Id + x.Trip.Image.Extentsion));
        }

    }
}
