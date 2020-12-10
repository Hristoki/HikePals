namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Web.ViewModels.Trips;

    public interface ITripsService
    {
        Task AddNewTrip(CreateTripInputViewModel model, string userId, string imageDirectoryPath);

        IEnumerable<TripViewModel> GetAllTrips();

        TripViewModel GetById(int tripId);

        EditTripViewModel GetEditViewModel(int tripId);

        Task UpdateAsync(EditTripViewModel model);

        Task DeleteAsync(int id);

    }
}
