﻿namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Administration.Events;
    using HikePals.Web.ViewModels.Events;
    using HikePals.Web.ViewModels.Trips;
    using HikePals.Web.ViewModels.Users;

    public class EventsService : IEventsService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;
        private readonly IDeletableEntityRepository<Trip> tripsRepository;
        private readonly IRepository<EventsUsers> eventUserRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public EventsService(
            IDeletableEntityRepository<Event> eventsRepository,
            IDeletableEntityRepository<Trip> tripsRepository,
            IRepository<EventsUsers> evenUserRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.eventsRepository = eventsRepository;
            this.tripsRepository = tripsRepository;
            this.eventUserRepository = evenUserRepository;
            this.userRepository = userRepository;
        }

        public async Task<int> CreateNewEvent(CreateEventInputViewModel input, string userId)
        {
            var @event = new Event
            {
                CreatedById = userId,
                Details = input.Details,
                EndTime = DateTime.Parse(input.EndTime, CultureInfo.InvariantCulture),
                StartTime = DateTime.Parse(input.StartTime, CultureInfo.InvariantCulture),
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
            return this.eventsRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Id == id) == null ? false : true;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.eventsRepository.AllAsNoTracking().OrderByDescending(x => x.CreatedOn).To<T>().ToList<T>();
        }

        /// <summary>
        ///    Returns the events a particular user has created.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>(string userId)
        {
            return this.eventsRepository.All().Where(x => x.CreatedById == userId).To<T>().ToList();
        }

        public IEnumerable<SingleEventAdminViewModel> GetAllWithDeleted()
        {
            return this.eventsRepository.AllWithDeleted().OrderByDescending(x => x.CreatedOn).To<SingleEventAdminViewModel>().ToList();
        }

        public T GetById<T>(int id)
        {
            return this.eventsRepository.All().Where(x => x.Id == id).To<T>().FirstOrDefault();
        }

        public T GetByIdWithDeleted<T>(int id)
        {
            return this.eventsRepository
                .AllAsNoTrackingWithDeleted()
                .Where(x => x.Id == id).
                To<T>()
                .FirstOrDefault();
        }

        public int GetAllEventsCount()
        {
            return this.eventsRepository.AllAsNoTracking().Count();
        }

        /// <summary>
        ///    Adds a request for joining the given event. Admins require no confirmation and automatically join if free slots are available.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <param name="isAdmin"></param>
        /// <returns></returns>
        public async Task RequestJoinEvent(int eventId, string userId, bool isAdmin)
        {
            if (userId == null)
            {
                throw new ArgumentException("User does not exist!");
            }

            if (!this.Exists(eventId))
            {
                throw new ArgumentException("Event does not exist!");
            }

            var eventUser = this.eventUserRepository.All().FirstOrDefault(x => x.UserId == userId && x.EventId == eventId);

            if (eventUser != null && !eventUser.PendingJoinRequest)
            {
                throw new ArgumentException("You have already joined the event!");
            }
            else if (eventUser != null && eventUser.PendingJoinRequest)
            {
                throw new ArgumentException("You have already sent a request to join!");
            }

            var @event = this.eventsRepository.All().FirstOrDefault(x => x.Id == eventId);

            if (@event.MaxGroupSize == @event.Participants.Count())
            {
                throw new ArgumentException("This event is full! You can't join this event! ");
            }

            eventUser = new EventsUsers { UserId = userId, EventId = eventId };


            if (@event.CreatedById == userId || isAdmin)
            {
                eventUser.PendingJoinRequest = false;
                @event.Participants.Add(eventUser);
            }
            else
            {
                eventUser.PendingJoinRequest = true;
                @event.Participants.Add(eventUser);
            }

            await this.eventsRepository.SaveChangesAsync();
        }

        public async Task LeaveEvent(string userId, int id)
        {
            var eventUser = this.eventUserRepository.All().FirstOrDefault(x => x.UserId == userId && x.EventId == id);

            if (eventUser == null)
            {
                throw new ArgumentException("This user has not joined the event!");
            }

            this.eventUserRepository.Delete(eventUser);
            await this.eventUserRepository.SaveChangesAsync();
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
            eventToUpdate.StartTime = DateTime.Parse(input.StartTime);
            eventToUpdate.EndTime = DateTime.Parse(input.EndTime);
            eventToUpdate.Details = input.Details;
            eventToUpdate.MaxGroupSize = input.MaxGroupSize;

            await this.eventsRepository.SaveChangesAsync();
        }

        public int GetUserEventsCount(string userId)
        {
            return this.eventsRepository.All().Where(x => x.CreatedById == userId).Count();
        }


        public async Task ApproveJoinRequest(string participantId, int eventId)
        {
            var user = this.userRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == participantId);
            if (user == null)
            {
                throw new ArgumentException("User does not exist!");
            }

            var eventUser = this.eventUserRepository.All().FirstOrDefault(x => x.UserId == participantId && x.EventId == eventId);
            var @event = this.eventsRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == eventId);

            if (@event == null)
            {
                throw new ArgumentException("This event does not exist!");
            }

            if (eventUser != null && !eventUser.PendingJoinRequest)
            {
                throw new ArgumentException("This user has already joined the event!");
            }

            if (@event.MaxGroupSize <= @event.Participants.Count())
            {
                throw new ArgumentException("Event is already full!");
            }

            eventUser.PendingJoinRequest = false;
            await this.eventUserRepository.SaveChangesAsync();
        }

        public async Task UndoJoinRequest(string userId, int id)
        {
            await this.LeaveEvent(userId, id);
        }

        public bool UserHasJoinedEvent(int id, string userId)
        {
            return this.eventUserRepository
                .All()
                .FirstOrDefault(x => x.EventId == id && x.UserId == userId && x.PendingJoinRequest == false) != null;
        }
    }
}
