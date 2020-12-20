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
    using HikePals.Web.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private static readonly string[] AllowedImageExtensions = { ".jpeg", ".jpg", ".png", ".tiff", ".gif" };
        private readonly IRepository<Trip> tripRepositry;
        private readonly IRepository<City> cityRepository;
        private readonly IRepository<Location> locationRepository;
        private readonly IRepository<Image> imageRepository;

        public TripsService(IRepository<Trip> tripRepositry, IRepository<City> cityRepository, IRepository<Location> locationRepository, IRepository<Image> imageRepository)
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
                     ImageUrl = x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion,
                 }).ToList();
        }

        public T GetById<T>(int tripId)
        {
            return this.tripRepositry
                .AllAsNoTracking()
                .Where(x => x.Id == tripId)
                .To<T>()
                //.Select(x =>
                //new TripViewModel
                //{
                //    Id = x.Id,
                //    Description = x.Description,
                //    Distance = x.Distance,
                //    Duration = x.Duration,
                //    Title = x.Title,
                //    UserId = x.CreatedByUser.Id,
                //    ImageUrl = x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion,
                //    LocationName = x.Location.Name,
                //    LocationCityName = x.Location.City.Name,
                //})
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
                   ImageUrl = x.Image == null ? "No image available" : "/images/trips/" + x.Image.Id + x.Image.Extentsion,
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

        public int GetCount()
        {
          return this.tripRepositry.AllAsNoTracking().Count();
        }
    }
}
