namespace HikePals.Services.Data.UserServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Trip> tripRepository;
        private readonly IDeletableEntityRepository<Event> eventRepository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepository, IDeletableEntityRepository<Trip> tripRepository, IDeletableEntityRepository<Event> eventRepository)
        {
            this.usersRepository = usersRepository;
            this.tripRepository = tripRepository;
            this.eventRepository = eventRepository;
        }

        public int GetAllUsersCount()
        {
            return this.usersRepository.All().Count();

        }

    }
}
