namespace HikePals.Web.ViewModels.Administration.Dashboard
{
    using HikePals.Web.ViewModels.Administration.Users;
    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;

    public class DashboardIndexViewModel
    {
        public int TripsCount { get; set; }

        public TripViewModel LastRegistertedTrip { get; set; }

        public int UsersCount { get; set; }

        public SingleUserAdminViewModel LastRegisteredUser { get; set; }

        public int EventsCount { get; set; }

        public EventViewModel LastRegisteredEvent { get; set; }
    }
}
