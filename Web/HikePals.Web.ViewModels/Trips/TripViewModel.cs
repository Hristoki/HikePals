namespace HikePals.Web.ViewModels.Trips
{
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TripViewModel : BaseTripViewModel, IMapFrom<Trip>
    {
        public string Description { get; set; }

        public string LocationCityName { get; set; }

        public string Duration { get; set; }

        public int Distance { get; set; }

        public string UserId { get; set; }
    }
}
