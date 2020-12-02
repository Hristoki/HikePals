namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using HikePals.Web.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private readonly IDeletableEntityRepository<Trip> tripRepositry;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Location> locationRepository;

        public TripsService(IDeletableEntityRepository<Trip> tripRepositry, IDeletableEntityRepository<City> cityRepository, IDeletableEntityRepository<Location> locationRepository)
        {
            this.tripRepositry = tripRepositry;
            this.cityRepository = cityRepository;
            this.locationRepository = locationRepository;
        }

        public async Task AddNewTrip(CreateTripInputViewModel model)
        {
            var destination = this.locationRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == model.Description);

            if (destination == null)
            {
                destination = new Location
                {
                    Name = model.Description,
                    CityId = model.CityId,
                    CategoryId = model.TypeOfDestinationId,
                };
            }

            var trip = new Trip
            {
                TripName = model.TripName,
                Description = model.Description,
                Destination = destination,
            };

            await this.tripRepositry.AddAsync(trip);
            await this.tripRepositry.SaveChangesAsync();
        }
    }
}
