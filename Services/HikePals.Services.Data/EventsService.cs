namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;

    public class EventsService : IEventsService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;
        private readonly IDeletableEntityRepository<Trip> tripsRepository;

        public EventsService(IDeletableEntityRepository<Event> eventsRepository, IDeletableEntityRepository<Trip> tripsRepository)
        {
            this.eventsRepository = eventsRepository;
            this.tripsRepository = tripsRepository;
        }

        public async Task CreateNewEvent(CreateEventInputViewModel input, string userId)
        {
            var eventEntity = new Event
            {
                CreatedById = userId,
                Details = input.Details,
                EndTime = input.EndTime,
                StartTime = input.StartTime,
                TransportId = input.TransportId,
                MaxGroupSize = input.MaxGroupSize,
                TripId = input.TripId, 
            }; 

            await this.eventsRepository.AddAsync(eventEntity);
            await this.eventsRepository.SaveChangesAsync();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventViewModel> GetAllTrips()
        {
            throw new NotImplementedException();
        }

        public T GetById<T>(int id)
        {
            return this.eventsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public EditEventViewModel GetEditViewModel(int tripId)
        {
            throw new NotImplementedException();
        }

        public CreateEventInputViewModel MapTripData(int tripId)
        {
           return this.tripsRepository.AllAsNoTracking().Where(x => x.Id == tripId).To<CreateEventInputViewModel>().FirstOrDefault();
        }

        public Task UpdateAsync(EditEventViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
