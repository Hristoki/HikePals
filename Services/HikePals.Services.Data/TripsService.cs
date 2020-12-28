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
    using HikePals.Services.Mapping;
    using HikePals.Web.ViewModels.Administration.Trips;
    using HikePals.Web.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private static readonly string[] AllowedImageExtensions = { ".jpeg", ".jpg", ".png", ".tiff", ".gif" };
        private readonly IDeletableEntityRepository<Trip> tripRepository;
        private readonly IRepository<City> cityRepository;
        private readonly IRepository<Location> locationRepository;
        private readonly IRepository<Image> imageRepository;

        public TripsService(IDeletableEntityRepository<Trip> tripRepositry, IRepository<City> cityRepository, IRepository<Location> locationRepository, IRepository<Image> imageRepository)
        {
            this.tripRepository = tripRepositry;
            this.cityRepository = cityRepository;
            this.locationRepository = locationRepository;
            this.imageRepository = imageRepository;
        }

        public async Task AddNewTrip(CreateTripInputViewModel model, string userId, string path)
        {
            var location = this.locationRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == model.Description);

            // TO DO: Refactor and Reuse

            if (location == null)
            {
                location = new Location
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
                Location = location,
                Duration = model.Duration,
                Distance = model.Distance,
                CreatedByUserId = userId,
            };

            //TO DO: Fix bug with create a trip without image

            string imageExtension = null;
            if (model.TripImage != null)
            {
             imageExtension = Path.GetExtension(model.TripImage.FileName.ToLower());
            }

            var image = new Image
            {
                Trip = trip,
                UserId = userId,
                Extentsion = imageExtension,
            };

            trip.Image = image;

            // Add image to File System
            var physicalPath = $"{path}/trips/{image.Id}{imageExtension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.TripImage.CopyToAsync(fileStream);

            await this.tripRepository.AddAsync(trip);
            await this.tripRepository.SaveChangesAsync();
        }

        public IEnumerable<TripViewModel> GetAllTrips()
        {
            return this.tripRepository.AllAsNoTracking()
                 .Select(x => new TripViewModel
                 {
                     LocationCategoryId = x.Location.CategoryId,
                     LocationCategoryName = x.Location.Category.Name,
                     Id = x.Id,
                     Title = x.Title,
                     ImageUrl = x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion,
                 }).ToList();
        }

        public T GetById<T>(int tripId)
        {
            return this.tripRepository
                .AllAsNoTracking()
                .Where(x => x.Id == tripId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(EditTripViewModel model)
        {
            var location = this.locationRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == model.LocationName);
            if (location == null)
            {
                location = new Location
                {
                    Name = model.LocationName,
                    CityId = model.CityId,
                    CategoryId = model.TypeOfDestinationId,
                };
            }


            var trip = this.tripRepository.AllWithDeleted().FirstOrDefault(x => x.Id == model.Id);
            trip.Title = model.Title;
            trip.Duration = model.Duration;
            trip.Description = model.Description;
            trip.Distance = model.Distance;
            trip.Location = location;

            await this.tripRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trip = this.tripRepository.All().FirstOrDefault(x => x.Id == id);
            this.tripRepository.Delete(trip);
            await this.tripRepository.SaveChangesAsync();
        }

        public int GetAllTripsCount()
        {
          return this.tripRepository.AllAsNoTracking().Count();
        }

        public T GetByIdWithDeleted<T>(int tripId)
        {
            return this.tripRepository
                .AllAsNoTrackingWithDeleted()
                .Where(x => x.Id == tripId)
                .To<T>()
                .FirstOrDefault();
        }

        public IEnumerable<SingleTripViewModel> GetAllTripsWithDeleted()
        {
            return this.tripRepository.AllWithDeleted().To<SingleTripViewModel>().ToList();
        }

        public bool Exists(int id)
        {
            return this.tripRepository.AllAsNoTracking().FirstOrDefault(x => x.Id == id) != null;
        }

        public async Task RestoreAsync(int id)
        {
            var eventEntity = await this.tripRepository.GetByIdWithDeletedAsync(id);
            eventEntity.IsDeleted = false;
            await this.tripRepository.SaveChangesAsync();
        }

        public int GetUserTripsCount(string id)
        {
            return this.tripRepository.AllAsNoTracking().Where(x => x.CreatedByUserId == id).Count();
        }

        public IEnumerable<T> GetAllUserTrips<T>(string userId)
        {
            return this.tripRepository.All().Where(x => x.CreatedByUserId == userId).To<T>().ToList();
        }

        private void IsTripNameAvailable(CreateTripInputViewModel model)
        {
            var isNameFree = this.tripRepository.AllAsNoTracking().FirstOrDefault(x => x.Title == model.Title);

            if (isNameFree != null)
            {
                throw new ArgumentException("This trip name is already available!");
            }
        }

    }


}
