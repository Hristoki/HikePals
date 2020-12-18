namespace HikePals.Web.ViewModels.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HikePals.Web.ViewModels.Users;

    public abstract class BaseEventViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string UserId { get; set; }


        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int MaxGroupSize { get; set; }

        public string TransportName { get; set; }

        public int TripId { get; set; }

        public TimeSpan Countdown => this.StartTime.Subtract(DateTime.Now);

        public TimeSpan TripDuration { get; set; }

        public int TripDistance { get; set; }

        public IEnumerable<UserViewModel> Participants { get; set; }
    }
}
