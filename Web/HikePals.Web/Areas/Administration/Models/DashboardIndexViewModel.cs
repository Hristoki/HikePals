using HikePals.Web.ViewModels.Events;
using HikePals.Web.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HikePals.Web.Areas.Administration.Models
{
    public class DashboardIndexViewModel
    {
        public int TripsCount { get; set; }

        public TripViewModel LastRegistertedTrip { get; set; }

        public int UsersCount { get; set; }

        public AdminAreaUserViewModel LastRegisteredUser { get; set; }

        public int EventsCount { get; set; }

        public EventViewModel LastRegisteredEvent { get; set; }
    }
}
