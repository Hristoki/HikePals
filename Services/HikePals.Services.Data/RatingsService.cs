using HikePals.Data.Common.Repositories;
using HikePals.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HikePals.Services.Data
{
    public class RatingsService : IRatingsService
    {
        private readonly IRepository<Rating> ratingRepository;
        private readonly IRepository<Trip> tripRepository;

        public RatingsService(IRepository<Rating> ratingRepository, IRepository<Trip> tripRepository)
        {
            this.ratingRepository = ratingRepository;
            this.tripRepository = tripRepository;
        }

        public double GetAverageRating(int tripId)
        {
            return this.ratingRepository.AllAsNoTracking().Where(x => x.TripId == tripId).Average(x => x.Value);
        }

        public async Task VoteAsync(string userId, int tripId, byte value)
        {
            var rating = this.ratingRepository.All().FirstOrDefault(x => x.UserId == userId && x.TripId == tripId);

            if (value <= 0 || value > 5)
            {
                throw new ArgumentException("Rating value should be between 1 and 5");
            }

            if (rating == null)
            {
                rating = new Rating() { TripId = tripId, UserId = userId};
                await this.ratingRepository.AddAsync(rating);
            }

            rating.Value = value;
            await this.ratingRepository.SaveChangesAsync();
        }
    }
}
