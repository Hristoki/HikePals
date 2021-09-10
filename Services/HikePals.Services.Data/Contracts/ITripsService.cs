namespace HikePals.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HikePals.Web.ViewModels.Administration.Trips;
    using HikePals.Web.ViewModels.Trips;

    public interface ITripsService
    {
        Task Create(CreateTripInputViewModel model, string userId, string imageDirectoryPath);

        AllTripsViewModel GetAll(
            string searchTerm,
            string categoy,
            TripSort sorting,
            int currentPage,
            int tripsPerPage);

        IEnumerable<SingleTripViewModel> GetAllWithDeleted();

        bool Exists(int id);

        T GetById<T>(int tripId);

        T GetByIdWithDeleted<T>(int tripId);

        Task UpdateAsync(CreateTripInputViewModel model, string userId, string imageDirPath);

        Task DeleteAsync(int id);

        int GetCount();

        Task RestoreAsync(int id);

        int GetUserCount(string userId);

        List<T> GetAllUserTrips<T>(string userId);

        IEnumerable<T> GetAllByCategory<T>(int categoryId);
    }
}
