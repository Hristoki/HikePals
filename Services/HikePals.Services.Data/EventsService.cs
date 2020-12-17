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
    using HikePals.Web.ViewModels.Users;

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

        public async Task DeleteAsync(int id)
        {
            var eventEntity = this.eventsRepository.All().FirstOrDefault(x => x.Id == id);
            this.eventsRepository.Delete(eventEntity);
            await this.eventsRepository.SaveChangesAsync();

        }

        public IEnumerable<EventViewModel> GetAllEvents()
        {
            return this.eventsRepository.AllAsNoTracking().To<EventViewModel>().ToList();
            //.Select(x => new EventViewModel
            //{Details = x.Details,
            //Id = x.Id,
            //StartTime = x.StartTime,
            //EndTime = x.EndTime,
            //MaxGroupSize = x.MaxGroupSize,
            //TripDistance = x.Trip.Distance,
            //TripDuration = x.Trip.Duration,
            //TripTitle = x.Trip.Title,
            //Image = $@"/images/trips/{x.Trip.Image.Id + x.Trip.Image.Extentsion}",
            //Participants = x.Participants.Select(x => new UserViewModel {Name = x.User.Name, CityName = x.User.City.Name, DateOfBirth = x.User.DateOfBirth, }).ToList(),
            //TransportName = x.Transport.Name,
            //})
            //.ToList();

            //
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

        public async Task UpdateAsync(EditEventViewModel input)
        {
            var eventToUpdate = this.eventsRepository
                .All()
                .Where(x => x.Id == input.Id)
                .FirstOrDefault();

            eventToUpdate.Title = input.Title;
            eventToUpdate.TransportId = input.TransportId;
            eventToUpdate.StartTime = input.StartTime;
            eventToUpdate.EndTime = input.EndTime;
            eventToUpdate.Details = input.Details;
            eventToUpdate.MaxGroupSize = input.MaxGroupSize;

            await this.eventsRepository.SaveChangesAsync();
        }
    }
}
