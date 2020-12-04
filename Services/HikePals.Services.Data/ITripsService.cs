using HikePals.Web.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HikePals.Services.Data
{
    public interface ITripsService
    {
        Task AddNewTrip(CreateTripInputViewModel model, string userId, string imageDirectoryPath);

    }
}
