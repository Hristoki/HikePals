namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class AllTripsViewModel
    {

        public ICollection<BaseTripViewModel> Trips { get; set; }
    }
}
