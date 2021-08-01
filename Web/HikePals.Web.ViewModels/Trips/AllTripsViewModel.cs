namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using HikePals.Data.Models;

    public class AllTripsViewModel
    {
        public const int TripPerPage = 3;

        public int TotalTripsCount { get; set; }

        public int CurrentPage { get; set; } = 1;

        [Display(Name = "Search")]
        public string SearchTerm { get; set; }

        public string Category { get; set; }

        public List<string> Categories { get; set; }

        public List<TripViewModel> Trips { get; set; }

        [Display(Name = "Order by")]

        public TripSort Sorting { get; set; }
    }
}
