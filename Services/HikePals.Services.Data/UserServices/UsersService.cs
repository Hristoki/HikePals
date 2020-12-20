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
        private readonly IRepository<ApplicationUser> usersRepository;

        public UsersService(IRepository<ApplicationUser> usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public int GetCount()
        {
            return this.usersRepository.All().Count();

        }
    }
}
