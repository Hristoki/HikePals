using HikePals.Data.Common.Models;
using System;
using System.Collections.Generic;

namespace HikePals.Data.Models
{
    public class Tag: BaseDeletableModel<int>
    {

        public Tag()
        {
            this.Trips = new HashSet<TripsTags>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<TripsTags> Trips { get; set; }
    }
}