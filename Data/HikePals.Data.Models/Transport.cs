namespace HikePals.Data.Models
{
    using System;
    using System.Collections.Generic;

    using HikePals.Data.Common.Models;

    public class Transport: BaseDeletableModel<int>
    {
        public Transport()
        {
            this.TripsTransport = new HashSet<Event>();
        }

        public string Name { get; set; }

        public virtual ICollection<Event> TripsTransport { get; set; }
    }
}
