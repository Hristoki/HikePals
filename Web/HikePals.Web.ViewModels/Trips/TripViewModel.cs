namespace HikePals.Web.ViewModels.Trips
{
    using System;

    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public class TripViewModel : BaseTripViewModel, IMapFrom<Trip>
    {
        public string LocationCityName { get; set; }

        public int LocationCategoryId { get; set; }

        public int Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public string Description { get; set; }
    }
}
