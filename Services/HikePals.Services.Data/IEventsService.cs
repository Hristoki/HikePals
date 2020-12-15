namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;

    public interface IEventsService
    {

        CreateEventInputViewModel MapTripData(int tripId);

        Task CreateNewEvent(CreateEventInputViewModel input, string userId);

        IEnumerable<EventViewModel> GetAllTrips();

        T GetById<T>(int tripId);

        EditEventViewModel GetEditViewModel(int eventId);

        Task UpdateAsync(EditEventViewModel model);

        Task DeleteAsync(int id);
    }
}
