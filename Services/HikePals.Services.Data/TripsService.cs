namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using HikePals.Data.Common.Repositories;
    using HikePals.Data.Models;
    using HikePals.Web.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private static readonly string[] AllowedImageExtensions = { ".jpeg", ".jpg", ".png", ".tiff", ".gif" };
        private readonly IDeletableEntityRepository<Trip> tripRepositry;
        private readonly IDeletableEntityRepository<City> cityRepository;
        private readonly IDeletableEntityRepository<Location> locationRepository;
        private readonly IDeletableEntityRepository<TripImage> imageRepository;

        public TripsService(IDeletableEntityRepository<Trip> tripRepositry, IDeletableEntityRepository<City> cityRepository, IDeletableEntityRepository<Location> locationRepository, IDeletableEntityRepository<TripImage> imageRepository)
        {
            this.tripRepositry = tripRepositry;
            this.cityRepository = cityRepository;
            this.locationRepository = locationRepository;
            this.imageRepository = imageRepository;
        }

        public async Task AddNewTrip(CreateTripInputViewModel model, string userId, string path)
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

            var imageExtension = Path.GetExtension(model.TripImage.FileName.ToLower());

            if (!AllowedImageExtensions.Contains(imageExtension))
            {
                throw new ArgumentException("Invalid image type!");
            }

            var image = new TripImage
            {
                Trip = trip,
                UserId = userId,
                Extentsion = imageExtension,
            };

            trip.TripImage = image;

            // Add image to File System
            var physicalPath = $"{path}/trips/{image.Id}.{imageExtension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.TripImage.CopyToAsync(fileStream);

            await this.tripRepositry.AddAsync(trip);
            await this.tripRepositry.SaveChangesAsync();
        }
    }
}
