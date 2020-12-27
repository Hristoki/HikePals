namespace HikePals.Web.ViewModels.Trips
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AutoMapper;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;

    public class TripViewModel : BaseTripViewModel, IMapFrom<Trip>, IHaveCustomMappings
    {

        public string LocationCityName { get; set; }

        public string UserId { get; set; }

        public int LocationCategoryId { get; set; }

        public int Distance { get; set; }

        public TimeSpan Duration { get; set; }

        public string Description { get; set; }

    }
}