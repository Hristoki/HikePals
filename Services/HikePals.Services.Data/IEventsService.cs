namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using HikePals.Data.Models;
    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;

    public interface IEventsService
    {

        CreateEventInputViewModel MapTripData(int tripId);

        Task<int> CreateNewEvent(CreateEventInputViewModel input, string userId);

        IEnumerable<EventViewModel> GetAll();

        IEnumerable<AdminEventViewModel> GetAllWithDeleted();

        T GetById<T>(int tripId);

        Task UpdateAsync(EditEventViewModel model);

        Task DeleteAsync(int id);

        Task JoinEvent(ApplicationUser user, int id);

        Task LeaveEvent(ApplicationUser user, int id);

        bool Exists(int id);

        Task RestoreAsync(int id);
        int GetCount();
    }
}
