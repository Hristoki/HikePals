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

        Task CreateNewEvent(CreateEventInputViewModel input, string userId);

        IEnumerable<EventViewModel> GetAllEvents();

        T GetById<T>(int tripId);

        Task UpdateAsync(EditEventViewModel model);

        Task DeleteAsync(int id);

        Task JoinEvent(ApplicationUser user, int id);
        Task LeaveEvent(ApplicationUser user, int id);
    }
}
