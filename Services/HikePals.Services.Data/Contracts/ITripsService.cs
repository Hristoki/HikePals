﻿namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using HikePals.Web.ViewModels.Administration.Trips;
    using HikePals.Web.ViewModels.Trips;

    public interface ITripsService
    {
        Task AddNewTrip(CreateTripInputViewModel model, string userId, string imageDirectoryPath);

        IEnumerable<T> GetAllTrips<T>();

        IEnumerable<SingleTripViewModel> GetAllTripsWithDeleted();

        bool Exists(int id);

        T GetById<T>(int tripId);

        T GetByIdWithDeleted<T>(int tripId);

        Task UpdateAsync(EditTripViewModel model);

        Task DeleteAsync(int id);

        int GetAllTripsCount();

        Task RestoreAsync(int id);

        int GetUserTripsCount(string userId);

        IEnumerable<T> GetAllUserTrips<T>(string userId);
    }
}
