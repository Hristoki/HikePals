namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllTripsViewModel
    {
        public const int TripPerPage = 2;

        public int TotalTripsCount { get; set; }

        public int CurrentPage { get; set; } = 1;

        public List<TripViewModel> Trips { get; set; }
    }
}
