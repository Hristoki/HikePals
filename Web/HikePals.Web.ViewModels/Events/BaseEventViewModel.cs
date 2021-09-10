namespace HikePals.Web.ViewModels.Events
{
    using System;

    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public abstract class BaseEventViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int MaxGroupSize { get; set; }

        public string TransportName { get; set; }

        public int TripId { get; set; }

        public TimeSpan TripDuration { get; set; }

        public int TripDistance { get; set; }

        public string ImageUrl { get; set; }

        public string CreatedById { get; set; }
    }
}
