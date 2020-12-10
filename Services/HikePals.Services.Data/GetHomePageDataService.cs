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
        private readonly IDeletableEntityRepository<Trip> tripRepositry;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IDeletableEntityRepository<Location> locationRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public GetHomePageDataService 
            (IDeletableEntityRepository<Trip> tripRepositry,
             IDeletableEntityRepository<ApplicationUser> userRepository,
             IDeletableEntityRepository<Location> locationRepository,
             IDeletableEntityRepository<Category> categoriesRepository)
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
