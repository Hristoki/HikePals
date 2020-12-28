using HikePals.Data.Common.Repositories;
using HikePals.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HikePals.Services.Data.UserServices
{
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
