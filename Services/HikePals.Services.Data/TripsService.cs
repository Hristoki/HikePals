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
                    Name = model.LocationName,
                    CityId = model.CityId,
                    CategoryId = model.CategoryId,
                };
            }

            this.IsTripNameAvailable(model);

            var trip = new Trip
            {
                Title = model.Title,
                Description = model.Description,
                Location = destination,
                Duration = model.Duration,
                Distance = model.Distance,
            };

            string imageExtension = null;
            if (model.TripImage != null)
            {
             imageExtension = Path.GetExtension(model.TripImage.FileName.ToLower());
            }

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
            var isNameFree = this.tripRepositry.AllAsNoTracking().FirstOrDefault(x => x.Title == model.Title);

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
                     LocationCategoryId = x.Location.CategoryId,
                     LocationCategoryName = x.Location.Category.Name,
                     Id = x.Id,
                     Title = x.Title,
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
                    Duration = x.Duration,
                    Title = x.Title,
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
                   Title = x.Title,
                   TripImageUrl = x.TripImage == null ? "No image available" : "/images/trips/" + x.TripImage.Id + x.TripImage.Extentsion,
                   CityId = x.Location.CityId,
                   TypeOfDestinationId = x.LocationId,
                   LocationName = x.Location.Name,

               })
               .FirstOrDefault();
        }

        public async Task UpdateAsync(EditTripViewModel model)
        {
            var location = this.locationRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == model.LocationName);
            if (location == null)
            {
                location = new Location
                {
                    Name = model.Description,
                    CityId = model.CityId,
                    CategoryId = model.TypeOfDestinationId,
                };
            }

            //await this.locationRepository.AddAsync(location);
            //await this.locationRepository.SaveChangesAsync();

            var trip = this.tripRepositry.All().FirstOrDefault(x => x.Id == model.Id);
            trip.Title = model.Title;
            trip.Duration = model.Duration;
            trip.Description = model.Description;
            trip.Distance = model.Distance;
            trip.Location = location;

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
