namespace HikePals.Web.ViewModels.Administration.Trips
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AllTripsAdminViewModel
    {
        public IEnumerable<SingleTripViewModel> Trips { get; set; }

        public string Title => "Events Overview";
    }
}
