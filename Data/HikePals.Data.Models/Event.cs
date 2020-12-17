namespace HikePals.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using HikePals.Data.Common.Models;

    public class Event : BaseDeletableModel<int>
    {
        public Event()
        {
            this.Participants = new HashSet<EventsUsers>();
            this.EventChat = new HashSet<Message>();
        }

        public string Title { get; set; }

        public string Details { get; set; }

        public string CreatedById { get; set; }

        public virtual ApplicationUser CreatedBy { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int MaxGroupSize { get; set; }

        public int TransportId { get; set; }

        public virtual Transport Transport { get; set; }

        public virtual ICollection<EventsUsers> Participants { get; set; }

        public virtual ICollection<Message> EventChat { get; set; }

        public int TripId { get; set; }

        public virtual Trip Trip { get; set; }
    }
}
