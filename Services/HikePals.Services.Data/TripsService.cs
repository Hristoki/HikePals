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

        public async Task Create(CreateTripInputViewModel model, string userId, string imageDirPath)
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

            this.IsNameAvailable(model);

            var trip = new Trip
            {
                Title = model.Title,
                Description = model.Description,
                Location = location,
                Duration = model.Duration,
                Distance = model.Distance,
                CreatedByUserId = userId,
            };

            Stream fileStream = await SaveImage(model, userId, imageDirPath, trip);

            await this.tripRepository.AddAsync(trip);
            await this.tripRepository.SaveChangesAsync();
        }

        private static async Task<Stream> SaveImage(CreateTripInputViewModel model, string userId, string imageDirPath, Trip trip)
        {
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
            var physicalPath = $"{imageDirPath}/trips/{image.Id}{imageExtension}";
            Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await model.TripImage.CopyToAsync(fileStream);
            return fileStream;
        }

        public AllTripsViewModel GetAll(string searchTerm, string category, TripSort sorting, int currentPage, int tripsPerPage)
        {
            var tripsQuery = this.tripRepository.All();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                tripsQuery = tripsQuery.Where(x =>
                x.Title.ToLower() == searchTerm.ToLower() ||
                x.Location.Name.ToLower().Contains(searchTerm.ToLower()) ||
                x.Description.ToLower().Contains(searchTerm.ToLower()) ||
                x.Title.Contains(searchTerm.ToLower()) ||
                x.Location.Description.ToLower().Contains(searchTerm.ToLower()) ||
                x.Location.Category.Name.ToLower() == searchTerm.ToLower() ||
                x.Location.City.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                tripsQuery = tripsQuery.Where(x => x.Location.Category.Name == category);
            }

            tripsQuery = sorting switch
            {
            TripSort.Category => tripsQuery.OrderBy(x => x.Location.Category.Name),
            TripSort.Location => tripsQuery.OrderBy(x => x.Location.Name),
            TripSort.Rating => tripsQuery.OrderByDescending(x => x.Rating.Average(x => x.Value)),
            TripSort.Date or _ => tripsQuery.OrderByDescending(x => x.Id),
        };

            var allTrips = tripsQuery.Count();
            var trips = tripsQuery.To<TripViewModel>().Skip((currentPage - 1) * tripsPerPage)
                .Take(tripsPerPage).ToList();

            return new AllTripsViewModel()
            {
             Trips = trips,
             TotalTripsCount = allTrips,
             CurrentPage = currentPage,
             SearchTerm = string.Empty,
             Sorting = sorting,
             Category = category,
            };
        }

        public IEnumerable<T> GetAllByCategory<T>(int categoryId)
        {
            return this.tripRepository.AllAsNoTracking().Where(x => x.Location.CategoryId == categoryId).To<T>().ToList();
        }

        public T GetById<T>(int tripId)
        {
            return this.tripRepository
                .AllAsNoTracking()
                .Where(x => x.Id == tripId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(CreateTripInputViewModel model, string userId, string imageDirPath)
        {
            var location = this.locationRepository.AllAsNoTracking().FirstOrDefault(x => x.Name == model.LocationName);
            if (location == null)
            {
                location = new Location
                {
                    Name = model.LocationName,
                    CityId = model.CityId,
                    CategoryId = model.CategoryId,
                };
            }

            var trip = this.tripRepository.AllWithDeleted().FirstOrDefault(x => x.Id == model.Id);
            trip.Title = model.Title;
            trip.Duration = model.Duration;
            trip.Description = model.Description;
            trip.Distance = model.Distance;
            trip.Location = location;

            if (model.TripImage != null)
            {
                Stream fileStream = await SaveImage(model, userId, imageDirPath, trip);
            }

            await this.tripRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var trip = this.tripRepository.All().FirstOrDefault(x => x.Id == id);
            this.tripRepository.Delete(trip);
            await this.tripRepository.SaveChangesAsync();
        }

        public int GetCount()
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

        public IEnumerable<SingleTripViewModel> GetAllWithDeleted()
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

        public int GetUserCount(string id)
        {
            return this.tripRepository.AllAsNoTracking().Where(x => x.CreatedByUserId == id).Count();
        }

        public List<T> GetAllUserTrips<T>(string userId)
        {
            return this.tripRepository.All().Where(x => x.CreatedByUserId == userId).To<T>().ToList();
        }

        private void IsNameAvailable(CreateTripInputViewModel model)
        {
            var isNameFree = this.tripRepository.AllAsNoTracking().FirstOrDefault(x => x.Title == model.Title);

            if (isNameFree != null)
            {
                throw new ArgumentException("This trip name is already available!");
            }
        }
    }
}
