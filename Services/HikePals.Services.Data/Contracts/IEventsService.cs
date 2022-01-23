namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Data.Models;
    using HikePals.Web.ViewModels.Administration.Events;
    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;

    public interface IEventsService
    {
        CreateEventInputViewModel MapTripData(int tripId);

        Task<int> CreateNewEvent(CreateEventInputViewModel input, string userId);

        IEnumerable<T> GetAll<T>();

        IEnumerable<SingleEventAdminViewModel> GetAllWithDeleted();

        T GetById<T>(int eventId);

        T GetByIdWithDeleted<T>(int eventId);

        Task UpdateAsync(EditEventViewModel model);

        Task DeleteAsync(int id);

        Task RequestJoinEvent(int eventId, string userId, bool isAdmin);

        Task ApproveJoinRequest(string participantId, int eventId);

        Task LeaveEvent(string user, int id);

        bool Exists(int id);

        Task RestoreAsync(int id);

        int GetAllEventsCount();

        int GetUserEventsCount(string userId);

        IEnumerable<T> GetAll<T>(string userId);

        Task UndoJoinRequest(string userId, int id);

        bool UserHasJoinedEvent(int id, string userId);
    }
}
