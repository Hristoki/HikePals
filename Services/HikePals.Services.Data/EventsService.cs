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
        private readonly IDeletableEntityRepository<EventsUsers> evenUserRepository;

        public EventsService(IDeletableEntityRepository<Event> eventsRepository, IDeletableEntityRepository<Trip> tripsRepository, IDeletableEntityRepository<EventsUsers> evenUserRepository)
        {
            this.eventsRepository = eventsRepository;
            this.tripsRepository = tripsRepository;
            this.evenUserRepository = evenUserRepository;
        }

        public async Task<int> CreateNewEvent(CreateEventInputViewModel input, string userId)
        {
            var @event = new Event
            {
                CreatedById = userId,
                Details = input.Details,
                EndTime = input.EndTime,
                StartTime = input.StartTime,
                TransportId = input.TransportId,
                MaxGroupSize = input.MaxGroupSize,
                TripId = input.TripId,
                Title = input.Title,
            };
            @event.Participants.Add(new EventsUsers { UserId = userId, EventId = @event.Id });
            await this.eventsRepository.AddAsync(@event);
            await this.eventsRepository.SaveChangesAsync();
            return @event.Id;

        }

        public async Task DeleteAsync(int id)
        {
            var eventEntity = this.eventsRepository.All().FirstOrDefault(x => x.Id == id);
            this.eventsRepository.Delete(eventEntity);
            await this.eventsRepository.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
           return this.eventsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id) == null ? false : true;
        }

        public IEnumerable<EventViewModel> GetAll()
        {
            return this.eventsRepository.AllAsNoTracking().To<EventViewModel>().ToList();
        }

        public IEnumerable<AdminEventViewModel> GetAllWithDeleted()
        {
            return this.eventsRepository.AllWithDeleted().To<AdminEventViewModel>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.eventsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public int GetCount()
        {
            return this.eventsRepository.AllAsNoTracking().Count();
        }

        public async Task JoinEvent(ApplicationUser user, int tripId)
        {
            var eventUser = this.evenUserRepository.All().FirstOrDefault(x => x.UserId == user.Id && x.EventId == tripId);

            if (eventUser != null)
            {
                throw new ArgumentException("This user has already joined the event!");
            }

            var eventEntity = this.eventsRepository.All().FirstOrDefault(x => x.Id == tripId);

            if (eventEntity.MaxGroupSize == eventEntity.Participants.Count())
            {
                throw new ArgumentException("This event is full! You can't join this event! ");
            }

            eventEntity.Participants.Add(new EventsUsers { UserId = user.Id, EventId = tripId });
            await this.eventsRepository.SaveChangesAsync();
        }

        public async Task LeaveEvent(ApplicationUser user, int id)
        {
            var eventUser = this.evenUserRepository.All().FirstOrDefault(x => x.UserId == user.Id && x.EventId == id);

            if (eventUser == null)
            {
                throw new ArgumentException("This user does not take part in the event!");
            }

            var eventEntity = this.eventsRepository.All().FirstOrDefault(x => x.Id == id);
            eventEntity.Participants.Remove(eventUser);

            await this.eventsRepository.SaveChangesAsync();
        }

        public CreateEventInputViewModel MapTripData(int tripId)
        {
           return this.tripsRepository.AllAsNoTracking().Where(x => x.Id == tripId).To<CreateEventInputViewModel>().FirstOrDefault();
        }

        public async Task RestoreAsync(int id)
        {
            var eventEntity = await this.eventsRepository.GetByIdWithDeletedAsync(id);
            eventEntity.IsDeleted = false;
            await this.eventsRepository.SaveChangesAsync();
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
