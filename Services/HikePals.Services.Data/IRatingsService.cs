namespace HikePals.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRatingsService
    {
        Task VoteAsync(string userId, int tripId, byte ratingValue);

        double GetAverageRating(int tripId);
    }
}
