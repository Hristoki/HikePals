using HikePals.Data.Models;
using HikePals.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace HikePals.Web.ViewModels.Trips
{
    public class TripViewModel : BaseTripViewModel
    {
        public int Id { get; set; }

        public string CityName { get; set; }

        public string UserId { get; set; }
    }
}
