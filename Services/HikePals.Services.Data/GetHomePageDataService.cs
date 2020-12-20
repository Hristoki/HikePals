using HikePals.Data.Common.Repositories;
using HikePals.Data.Models;
using HikePals.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HikePals.Services.Data
{
    public class GetHomePageDataService : IGetHomePageDataService
    {
        private readonly IRepository<Trip> tripRepositry;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Location> locationRepository;
        private readonly IRepository<Category> categoriesRepository;

        public GetHomePageDataService 
            (IRepository<Trip> tripRepositry,
             IRepository<ApplicationUser> userRepository,
             IRepository<Location> locationRepository,
             IRepository<Category> categoriesRepository)
        {
            this.tripRepositry = tripRepositry;
            this.userRepository = userRepository;
            this.locationRepository = locationRepository;
            this.categoriesRepository = categoriesRepository;
        }

        public IndexViewModel GetCounts()
        {

            var data = new IndexViewModel()
            {
                TripsCount = this.tripRepositry.All().Count(),
                LocationsCount = this.locationRepository.All().Count(),
                UsersCount = this.userRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
            };

            return data;
        }
    }
}
