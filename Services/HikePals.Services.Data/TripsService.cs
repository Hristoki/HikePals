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
    using HikePals.Services.Mapping;

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

            // TO DO: Refactor and Reuse
            if (destination == null)
            {
                destination = new Location
                {
                    Name = model.Destination,
                    CityId = model.CityId,
                    CategoryId = model.TypeOfDestinationId,
                };
            }

            this.IsTripNameAvailable(model);

            var trip = new Trip
            {
                TripName = model.TripName,
                Description = model.Description,
                Destination = destination,
                Duration = model.Duration,
                Distance = model.Distance,
            };

            var imageExtension = Path.GetExtension(model.TripImage.FileName.ToLower());

            //if (!AllowedImageExtensions.Contains(imageExtension))
            //{
            //    throw new ArgumentException("Invalid image type!");
            //}

            var image = new TripImage
            {
                Trip = trip,
                UserId = userId,
                Extentsion = imageExtension,
            };

            trip.TripImage = image;

            // Add image to File System
            var physicalPath = $"{path}/trips/{image.Id}{imageExtension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.TripImage.CopyToAsync(fileStream);

            await this.tripRepositry.AddAsync(trip);
            await this.tripRepositry.SaveChangesAsync();
        }

        private void IsTripNameAvailable(CreateTripInputViewModel model)
        {
            var isNameFree = this.tripRepositry.AllAsNoTracking().FirstOrDefault(x => x.TripName == model.TripName);

            if (isNameFree != null)
            {
                throw new ArgumentException("This trip name is already available!");
            }
        }

        public IEnumerable<TripViewModel> GetAllTrips()
        {
            return this.tripRepositry.AllAsNoTracking()
                 .Select(x => new TripViewModel
                 {
                     LocationCategoryId = x.Destination.CategoryId,
                     LocationCategoryName = x.Destination.Category.Name,
                     Id = x.Id,
                     Name = x.TripName,
                     TripImageUrl = x.TripImage == null ? "No image available" : "/images/trips/" + x.TripImage.Id + x.TripImage.Extentsion,
                 }).ToList();
        }

        public TripViewModel GetById(int tripId)
        {
            return this.tripRepositry
                .AllAsNoTracking()
                .Where(x => x.Id == tripId)
                .Select(x =>
                new TripViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Distance = x.Distance,
                    Duration = x.Duration.ToString(),
                    Name = x.TripName,
                    UserId = x.CreatedByUser.Id,
                    TripImageUrl = x.TripImage == null ? "No image available" : "/images/trips/" + x.TripImage.Id + x.TripImage.Extentsion,
                })
                .FirstOrDefault();
        }

        public EditTripViewModel GetEditViewModel(int tripId)
        {
            return this.tripRepositry
               .AllAsNoTracking()
               .Where(x => x.Id == tripId)
               .Select(x =>
               new EditTripViewModel
               {
                   Id = x.Id,
                   Description = x.Description,
                   Distance = x.Distance,
                   Duration = x.Duration,
                   TripName = x.TripName,
                   TripImageUrl = x.TripImage == null ? "No image available" : "/images/trips/" + x.TripImage.Id + x.TripImage.Extentsion,
                   CityId = x.Destination.CityId,
                   TypeOfDestinationId = x.DestinationId,
                   Destination = x.Destination.Name,

               })
               .FirstOrDefault();
        }

        public async Task UpdateAsync(EditTripViewModel model)
        {
            var destination = this.locationRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == model.Destination);
            if (destination == null)
            {
                destination = new Location
                {
                    Name = model.Description,
                    CityId = model.CityId,
                    CategoryId = model.TypeOfDestinationId,
                };
            }

            await this.locationRepository.AddAsync(destination);
            await this.locationRepository.SaveChangesAsync();

            var trip = this.tripRepositry.All().FirstOrDefault(x => x.Id == model.Id);
            trip.TripName = model.TripName;
            trip.Duration = model.Duration;
            trip.Description = model.Description;
            trip.Distance = model.Distance;
            trip.Destination.Id = destination.Id;

            await this.tripRepositry.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trip = this.tripRepositry.All().FirstOrDefault(x => x.Id == id);
            this.tripRepositry.Delete(trip);
            await this.tripRepositry.SaveChangesAsync();

        }
    }
}
